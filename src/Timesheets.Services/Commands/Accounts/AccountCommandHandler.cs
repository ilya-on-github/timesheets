using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timesheets.Services.Queries.Accounts;

namespace Timesheets.Services.Commands.Accounts
{
    public class AccountCommandHandler : IRequestHandler<CreateAccountCommand, IAccount>
    {
        private readonly IAccountRepository _accountRepository;

        public AccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public async Task<IAccount> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account(request.Name);

            await _accountRepository.Save(account, cancellationToken);

            return account;
        }
    }
}