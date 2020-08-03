using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Timesheets.Persistence.Models;
using Timesheets.Persistence.Queries;
using Timesheets.Services.Commands.Issues;

namespace Timesheets.Persistence.Repositories
{
    public class IssueRepository : Repository, IIssueRepository
    {
        public IssueRepository(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<Issue> Get(Guid id, CancellationToken cancellationToken)
        {
            var dbIssue = await DbContext.Issues
                .AsNoTracking()
                .FirstOrDefaultAsync(IssueSpecs.ById(id), cancellationToken);

            return Mapper.Map<Issue>(dbIssue);
        }

        public async Task Save(Issue issue, CancellationToken cancellationToken)
        {
            var dbIssue = Mapper.Map<DbIssue>(issue);

            var existingIssue = await DbContext.Issues
                .FirstOrDefaultAsync(IssueSpecs.ById(dbIssue.Id), cancellationToken);

            if (existingIssue == null)
            {
                await DbContext.Issues.AddAsync(dbIssue, cancellationToken);
            }
            else
            {
                DbContext.Entry(existingIssue).CurrentValues.SetValues(dbIssue);
            }
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            var existingIssue = await DbContext.Issues
                .FirstOrDefaultAsync(IssueSpecs.ById(id), cancellationToken);

            if (existingIssue != null)
            {
                DbContext.Issues.Remove(existingIssue);
            }
        }
    }
}