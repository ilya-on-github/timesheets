using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Timesheets.Services.Queries.Accounts;

namespace Timesheets.Persistence.Queries
{
    public class AccountQueryHandler : QueryHandler, IRequestHandler<AccountQuery, IEnumerable<IAccount>>,
        IRequestHandler<SingleAccountQuery, IAccount>
    {
        public AccountQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<IEnumerable<IAccount>> Handle(AccountQuery request, CancellationToken cancellationToken)
        {
            var items = await DbContext.Accounts
                .AsNoTracking()
                .Where(AccountSpecs.ByFilter(request.Filter))
                .OrderBy(x => x.Name)
                .Page(request.Filter)
                .ToListAsync(cancellationToken);

            return Mapper.Map<IEnumerable<IAccount>>(items);
        }

        public async Task<IAccount> Handle(SingleAccountQuery request, CancellationToken cancellationToken)
        {
            var account = await DbContext.Accounts
                .FirstOrDefaultAsync(AccountSpecs.ById(request.Id), cancellationToken);

            return Mapper.Map<IAccount>(account);
        }
    }
}