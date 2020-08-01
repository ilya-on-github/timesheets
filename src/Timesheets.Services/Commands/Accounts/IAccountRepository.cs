using System;
using System.Threading;
using System.Threading.Tasks;

namespace Timesheets.Services.Commands.Accounts
{
    public interface IAccountRepository
    {
        Task<Account> Get(Guid id, CancellationToken cancellationToken);
        Task Save(Account account, CancellationToken cancellationToken);
    }
}