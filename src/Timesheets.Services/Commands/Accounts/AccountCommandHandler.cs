using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Timesheets.Services.Commands.Accounts
{
    // ReSharper disable once UnusedType.Global
    public class AccountCommandHandler : IRequestHandler<CreateAccountCommand, Guid>,
        IRequestHandler<UpdateAccountCommand>,
        IRequestHandler<DeleteAccountCommand>
    {
        private readonly IAccountRepository _accountRepository;

        public AccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account(request.Name);

            await _accountRepository.Save(account, cancellationToken);

            return account.Id;
        }

        public async Task<Unit> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.Get(request.Id, cancellationToken);

            account.Update(request.Name);

            await _accountRepository.Save(account, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            await _accountRepository.Delete(request.Id, cancellationToken);

            return Unit.Value;
        }
    }
}