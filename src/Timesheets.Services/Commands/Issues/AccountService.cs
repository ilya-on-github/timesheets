using System;
using System.Threading;
using System.Threading.Tasks;
using Timesheets.Services.Commands.Accounts;

namespace Timesheets.Services.Commands.Issues
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public async Task<AccountDescriptor> GetAccountDescriptor(Guid? id, CancellationToken cancellationToken)
        {
            if (!id.HasValue)
            {
                return null;
            }

            var account = await _accountRepository.Get(id.Value, cancellationToken);
            return account?.Descriptor();
        }
    }
}