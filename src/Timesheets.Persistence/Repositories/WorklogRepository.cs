using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Timesheets.Persistence.Models;
using Timesheets.Persistence.Queries;
using Timesheets.Services.Commands.Worklogs;

namespace Timesheets.Persistence.Repositories
{
    public class WorklogRepository : Repository, IWorklogRepository
    {
        public WorklogRepository(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<Worklog> Get(Guid id, CancellationToken cancellationToken)
        {
            var dbWorklog = await DbContext.Worklogs
                .AsNoTracking()
                .FirstOrDefaultAsync(WorklogSpecs.ById(id), cancellationToken);

            return Mapper.Map<Worklog>(dbWorklog);
        }

        public async Task Save(Worklog worklog, CancellationToken cancellationToken)
        {
            var dbWorklog = Mapper.Map<DbWorklog>(worklog);

            var existingWorklog = await DbContext.Worklogs
                .FirstOrDefaultAsync(WorklogSpecs.ById(dbWorklog.Id), cancellationToken);

            if (existingWorklog == null)
            {
                await DbContext.Worklogs.AddAsync(dbWorklog, cancellationToken);
            }
            else
            {
                DbContext.Entry(existingWorklog).CurrentValues.SetValues(dbWorklog);
            }
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            var existingWorklog = await DbContext.Worklogs
                .FirstOrDefaultAsync(WorklogSpecs.ById(id), cancellationToken);

            if (existingWorklog != null)
            {
                DbContext.Worklogs.Remove(existingWorklog);
            }
        }
    }
}