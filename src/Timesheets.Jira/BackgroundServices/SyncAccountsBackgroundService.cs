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
    public class SyncAccountsBackgroundService : BackgroundService
    {
        private readonly IOptionsMonitor<SyncAccountsBackgroundServiceOptions> _optionsMonitor;
        private readonly Jira _jira;
        private readonly ILogger<SyncAccountsBackgroundService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SyncAccountsBackgroundService(IOptionsMonitor<SyncAccountsBackgroundServiceOptions> optionsMonitor,
            IServiceScopeFactory serviceScopeFactory, Jira jira, ILogger<SyncAccountsBackgroundService> logger)
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

                var addedCount = 0;
                var updatedCount = 0;
                var deletedCount = 0;

                try
                {
                    var jiraAccounts = await _jira.GetAccounts(stoppingToken);
                    var retrievedCount = jiraAccounts.Count;

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                        var dbAccounts = await dbContext.Accounts.ToListAsync(stoppingToken);

                        foreach (var account in jiraAccounts)
                        {
                            var now = DateTimeOffset.Now;

                            var dbAccount = dbAccounts.FirstOrDefault(x => x.Id.Equals(account.Id));
                            if (dbAccount == null)
                            {
                                await dbContext.Accounts.AddAsync(new DbAccount
                                {
                                    Id = account.Id,
                                    Key = account.Key,
                                    Name = account.Name,
                                    Checked = now,
                                    Updated = now
                                }, stoppingToken);

                                addedCount++;
                            }
                            else
                            {
                                if (!string.Equals(account.Key, dbAccount.Key, StringComparison.InvariantCulture) ||
                                    !string.Equals(account.Name, dbAccount.Name, StringComparison.InvariantCulture))
                                {
                                    dbAccount.Key = account.Key;
                                    dbAccount.Name = account.Name;
                                    dbAccount.Updated = now;

                                    updatedCount++;
                                }

                                dbAccount.Checked = now;

                                if (dbAccount.IsRemoved)
                                {
                                    dbAccount.IsRemoved = false;
                                }

                                dbAccounts.Remove(dbAccount);
                            }
                        }

                        foreach (var dbAccount in dbAccounts.Where(dbAccount => !dbAccount.IsRemoved))
                        {
                            dbAccount.IsRemoved = true;

                            deletedCount++;
                        }

                        await dbContext.SaveChangesAsync(stoppingToken);
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

        private SyncAccountsBackgroundServiceOptions GetOptions()
        {
            var currentOptions = _optionsMonitor.CurrentValue;

            var defaultResetTimeout = TimeSpan.FromMinutes(1);
            var resetTimeout = currentOptions.ResetTimeout > TimeSpan.Zero
                ? currentOptions.ResetTimeout
                : defaultResetTimeout;

            return new SyncAccountsBackgroundServiceOptions
            {
                ResetTimeout = resetTimeout
            };
        }
    }

    public class SyncAccountsBackgroundServiceOptions
    {
        public TimeSpan ResetTimeout { get; set; }
    }
}