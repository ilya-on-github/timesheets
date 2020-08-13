using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Timesheets.Jira.Persistence;
using Timesheets.Jira.Persistence.Models;

namespace Timesheets.Jira.BackgroundServices
{
    public class SyncWorklogsBackgroundService : BackgroundService
    {
        private readonly IOptionsMonitor<SyncWorklogsBackgroundServiceOptions> _optionsMonitor;
        private readonly Jira _jira;
        private readonly ILogger<SyncWorklogsBackgroundService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SyncWorklogsBackgroundService(IOptionsMonitor<SyncWorklogsBackgroundServiceOptions> optionsMonitor,
            IServiceScopeFactory serviceScopeFactory, Jira jira, ILogger<SyncWorklogsBackgroundService> logger)
        {
            _optionsMonitor = optionsMonitor ?? throw new ArgumentNullException(nameof(optionsMonitor));
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            _jira = jira ?? throw new ArgumentNullException(nameof(jira));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Started.");

                var options = GetOptions();

                try
                {
                    var retrievedCount = 0;
                    var addedCount = 0;
                    var updatedCount = 0;
                    var deletedCount = 0;

                    var today = DateTime.Today;
                    var month = today.Month;
                    var currentDay = new DateTime(today.Year, today.Month, 1);

                    while (currentDay.Month == month)
                    {
                        var currentDayStart = new DateTimeOffset(currentDay, _jira.TimeOffset);
                        var nextDayStart = new DateTimeOffset(currentDay.AddDays(1), _jira.TimeOffset);

                        var worklogs = await _jira.FindWorklogs(currentDay, currentDay, stoppingToken);
                        retrievedCount += worklogs.Count;

                        using (var scope = _serviceScopeFactory.CreateScope())
                        {
                            var dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                            await using (var transaction =
                                await dbContext.Database.BeginTransactionAsync(stoppingToken))
                            {
                                var worklogIds = worklogs.Select(x => x.TempoWorklogId).ToArray();
                                var dbWorklogs = await dbContext.Worklogs
                                    .Where(x => worklogIds.Contains(x.TempoWorklogId))
                                    .ToListAsync(stoppingToken);

                                foreach (var worklog in worklogs)
                                {
                                    var issue = worklog.Issue;
                                    var dbIssue = await
                                        dbContext.Issues.FirstOrDefaultAsync(x => x.Id == issue.Id, stoppingToken);

                                    if (dbIssue == null)
                                    {
                                        dbIssue = new DbIssue
                                        {
                                            Id = issue.Id,
                                            Key = issue.Key,
                                            Summary = issue.Summary,
                                            AccountKey = issue.AccountKey
                                        };

                                        await dbContext.Issues.AddAsync(dbIssue, stoppingToken);
                                        await dbContext.SaveChangesAsync(stoppingToken);
                                    }
                                    else
                                    {
                                        dbIssue.Key = issue.Key;
                                        dbIssue.Summary = issue.Summary;
                                        dbIssue.AccountKey = issue.AccountKey;
                                    }

                                    var now = DateTimeOffset.Now;
                                    var worklogStarted = new DateTimeOffset(worklog.Started, _jira.TimeOffset);

                                    var dbWorklog =
                                        dbWorklogs.FirstOrDefault(x => x.TempoWorklogId == worklog.TempoWorklogId);
                                    if (dbWorklog == null)
                                    {
                                        await dbContext.Worklogs.AddAsync(new DbWorklog
                                        {
                                            TempoWorklogId = worklog.TempoWorklogId,
                                            Worker = worklog.Worker,
                                            IssueId = worklog.Issue.Id,
                                            Started = worklogStarted,
                                            TimeSpentSeconds = worklog.TimeSpentSeconds,
                                            Comment = worklog.Comment,
                                            Checked = now,
                                            Updated = now
                                        }, stoppingToken);

                                        addedCount++;
                                    }
                                    else
                                    {
                                        if (!string.Equals(worklog.Worker, dbWorklog.Worker,
                                                StringComparison.InvariantCulture) ||
                                            worklog.Issue.Id != dbWorklog.IssueId ||
                                            worklogStarted != dbWorklog.Started ||
                                            worklog.TimeSpentSeconds != dbWorklog.TimeSpentSeconds ||
                                            !string.Equals(worklog.Comment, dbWorklog.Comment,
                                                StringComparison.InvariantCulture))
                                        {
                                            dbWorklog.Worker = worklog.Worker;
                                            dbWorklog.IssueId = worklog.Issue.Id;
                                            dbWorklog.Started = worklogStarted;
                                            dbWorklog.TimeSpentSeconds = worklog.TimeSpentSeconds;
                                            dbWorklog.Comment = worklog.Comment;
                                            dbWorklog.Updated = now;

                                            updatedCount++;
                                        }

                                        dbWorklog.Checked = now;

                                        if (dbWorklog.IsRemoved)
                                        {
                                            dbWorklog.IsRemoved = false;
                                        }
                                    }
                                }

                                var otherCurrentDayWorklogs = await dbContext.Worklogs
                                    .Where(x => x.Started >= currentDayStart && x.Started < nextDayStart &&
                                                !x.IsRemoved && !worklogIds.Contains(x.TempoWorklogId))
                                    .ToListAsync(stoppingToken);

                                foreach (var dbWorklog in otherCurrentDayWorklogs)
                                {
                                    dbWorklog.IsRemoved = true;

                                    deletedCount++;
                                }

                                await dbContext.SaveChangesAsync(stoppingToken);

                                await transaction.CommitAsync(stoppingToken);
                            }
                        }

                        currentDay = currentDay.AddDays(1);
                    }

                    _logger.LogInformation(
                        $"Finished successfully. Stats: {retrievedCount} retrieved, {addedCount} added, {updatedCount} updated, {deletedCount} deleted.");
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Finished with error.");
                }

                _logger.LogInformation($"The next run will start in {options.ResetTimeout.TotalSeconds} sec.");
                await Task.Delay(options.ResetTimeout, stoppingToken);
            }
        }

        private SyncWorklogsBackgroundServiceOptions GetOptions()
        {
            var currentOptions = _optionsMonitor.CurrentValue;

            var defaultResetTimeout = TimeSpan.FromMinutes(1);
            var resetTimeout = currentOptions.ResetTimeout > TimeSpan.Zero
                ? currentOptions.ResetTimeout
                : defaultResetTimeout;

            return new SyncWorklogsBackgroundServiceOptions
            {
                ResetTimeout = resetTimeout
            };
        }
    }

    public class SyncWorklogsBackgroundServiceOptions
    {
        public TimeSpan ResetTimeout { get; set; }
    }
}