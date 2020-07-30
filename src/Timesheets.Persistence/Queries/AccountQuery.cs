using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Timesheets.Services.Queries.Accounts;

namespace Timesheets.Persistence.Queries
{
    public class AccountQuery : Query, IAccountQuery
    {
        public AccountQuery(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<IEnumerable<IAccount>> Execute(AccountFilter filter, CancellationToken cancellationToken)
        {
            var items = await DbContext.Accounts
                .AsNoTracking()
                .Where(AccountSpecs.ByFilter(filter))
                .OrderBy(x => x.Name)
                .Page(filter)
                .ToListAsync(cancellationToken);

            return Mapper.Map<IEnumerable<IAccount>>(items);
        }
    }
}