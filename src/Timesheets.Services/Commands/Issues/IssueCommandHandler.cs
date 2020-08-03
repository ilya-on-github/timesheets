using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Timesheets.Services.Commands.Issues
{
    // ReSharper disable once UnusedType.Global
    public class IssueCommandHandler : IRequestHandler<CreateIssueCommand, Guid>,
        IRequestHandler<UpdateIssueCommand>, IRequestHandler<DeleteIssueCommand>
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IAccountService _accountService;

        public IssueCommandHandler(IIssueRepository issueRepository, IAccountService accountService)
        {
            _issueRepository = issueRepository ?? throw new ArgumentNullException(nameof(issueRepository));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public async Task<Guid> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountService.GetAccountDescriptor(request.AccountId, cancellationToken);

            var issue = new Issue(request.Summary, request.Description, account);
            await _issueRepository.Save(issue, cancellationToken);

            return issue.Id;
        }

        public async Task<Unit> Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountService.GetAccountDescriptor(request.AccountId, cancellationToken);
            var issue = await _issueRepository.Get(request.Id, cancellationToken);

            issue.Update(request.Summary, request.Description, account);

            await _issueRepository.Save(issue, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteIssueCommand request, CancellationToken cancellationToken)
        {
            await _issueRepository.Delete(request.Id, cancellationToken);

            return Unit.Value;
        }
    }
}