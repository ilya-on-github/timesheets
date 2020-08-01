using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timesheets.Services.Queries.Accounts;

namespace Timesheets.Services.Commands.Accounts
{
    // ReSharper disable once UnusedType.Global
    public class AccountCommandHandler : IRequestHandler<CreateAccountCommand, IAccount>,
        IRequestHandler<UpdateAccountCommand, IAccount>
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

        public async Task<IAccount> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.Get(request.Id, cancellationToken);

            account.Update(request.Name);

            await _accountRepository.Save(account, cancellationToken);

            return account;
        }
    }
}