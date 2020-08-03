using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Timesheets.Services.Queries.Issues;

namespace Timesheets.Persistence.Queries
{
    // ReSharper disable once UnusedType.Global
    public class IssueQueryHandler : Query, IRequestHandler<IssueQuery, IEnumerable<IIssue>>,
        IRequestHandler<SingleIssueQuery, IIssue>
    {
        public IssueQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<IEnumerable<IIssue>> Handle(IssueQuery request, CancellationToken cancellationToken)
        {
            var items = await DbContext.Issues
                .AsNoTracking()
                .Include(x => x.Account)
                .Where(IssueSpecs.ByFilter(request.Filter))
                .OrderBy(x => x.Summary)
                .Page(request.Filter)
                .ToListAsync(cancellationToken);

            return Mapper.Map<IEnumerable<IIssue>>(items);
        }

        public async Task<IIssue> Handle(SingleIssueQuery request, CancellationToken cancellationToken)
        {
            var issue = await DbContext.Issues
                .AsNoTracking()
                .Include(x => x.Account)
                .FirstOrDefaultAsync(IssueSpecs.ById(request.Id), cancellationToken);

            return Mapper.Map<IIssue>(issue);
        }
    }
}