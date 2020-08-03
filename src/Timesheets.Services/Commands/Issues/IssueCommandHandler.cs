using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timesheets.Services.Queries.Issues;

namespace Timesheets.Services.Commands.Issues
{
    public class IssueCommandHandler : IRequestHandler<CreateIssueCommand, IIssue>,
        IRequestHandler<UpdateIssueCommand, IIssue>, IRequestHandler<DeleteIssueCommand>
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IMediator _mediator;
        private readonly IAccountService _accountService;

        public IssueCommandHandler(IIssueRepository issueRepository, IMediator mediator, IAccountService accountService)
        {
            _issueRepository = issueRepository ?? throw new ArgumentNullException(nameof(issueRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public async Task<IIssue> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountService.GetAccountDescriptor(request.AccountId, cancellationToken);

            var issue = new Issue(request.Summary, request.Description, account);
            await _issueRepository.Save(issue, cancellationToken);

            return await GetIssue(issue.Id, cancellationToken);
        }

        public async Task<IIssue> Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountService.GetAccountDescriptor(request.AccountId, cancellationToken);
            var issue = await _issueRepository.Get(request.Id, cancellationToken);

            issue.Update(request.Summary, request.Description, account);

            await _issueRepository.Save(issue, cancellationToken);

            return await GetIssue(issue.Id, cancellationToken);
        }

        public async Task<Unit> Handle(DeleteIssueCommand request, CancellationToken cancellationToken)
        {
            await _issueRepository.Delete(request.Id, cancellationToken);

            return Unit.Value;
        }

        private async Task<IIssue> GetIssue(Guid id, CancellationToken cancellationToken)
        {
            var issue = await _mediator.Send(new SingleIssueQuery(id), cancellationToken);
            return issue;
        }
    }
}