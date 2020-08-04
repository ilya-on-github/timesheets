using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Timesheets.Services.Queries.Worklogs;

namespace Timesheets.Persistence.Queries
{
    public class WorklogQueryHandler : QueryHandler, IRequestHandler<WorklogQuery, IEnumerable<IWorklog>>,
        IRequestHandler<SingleWorklogQuery, IWorklog>
    {
        public WorklogQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<IEnumerable<IWorklog>> Handle(WorklogQuery request, CancellationToken cancellationToken)
        {
            var items = await DbContext.Worklogs
                .AsNoTracking()
                .Include(x => x.Employee)
                .Include(x => x.Issue).ThenInclude(x => x.Account)
                .Where(WorklogSpecs.ByFilter(request.Filter))
                .OrderByDescending(x => x.Started)
                .Page(request.Filter)
                .ToListAsync(cancellationToken);

            return Mapper.Map<IEnumerable<IWorklog>>(items);
        }

        public async Task<IWorklog> Handle(SingleWorklogQuery request, CancellationToken cancellationToken)
        {
            var worklog = await DbContext.Worklogs
                .AsNoTracking()
                .Include(x => x.Employee)
                .Include(x => x.Issue).ThenInclude(x => x.Account)
                .FirstOrDefaultAsync(WorklogSpecs.ById(request.Id), cancellationToken);

            return Mapper.Map<IWorklog>(worklog);
        }
    }
}