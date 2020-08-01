using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Timesheets.Persistence.Models;
using Timesheets.Persistence.Queries;
using Timesheets.Services.Commands.Accounts;

namespace Timesheets.Persistence.Repositories
{
    public class AccountRepository : Repository, IAccountRepository
    {
        public AccountRepository(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<Account> Get(Guid id, CancellationToken cancellationToken)
        {
            var dbAccount = await DbContext.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(AccountSpecs.ById(id), cancellationToken);

            return Mapper.Map<Account>(dbAccount);
        }

        public async Task Save(Account account, CancellationToken cancellationToken)
        {
            var dbAccount = Mapper.Map<DbAccount>(account);

            var existingAccount = await DbContext.Accounts
                .FirstOrDefaultAsync(AccountSpecs.ById(dbAccount.Id), cancellationToken);

            if (existingAccount == null)
            {
                await DbContext.Accounts.AddAsync(dbAccount, cancellationToken);
            }
            else
            {
                DbContext.Entry(existingAccount).CurrentValues.SetValues(dbAccount);
            }
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            var existingAccount = await DbContext.Accounts
                .FirstOrDefaultAsync(AccountSpecs.ById(id), cancellationToken);

            if (existingAccount != null)
            {
                DbContext.Accounts.Remove(existingAccount);
            }
        }
    }
}