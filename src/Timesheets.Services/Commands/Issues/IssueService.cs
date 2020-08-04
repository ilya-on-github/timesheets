using System;
using System.Threading;
using System.Threading.Tasks;

namespace Timesheets.Services.Commands.Issues
{
    public class IssueService : IIssueService
    {
        private readonly IIssueRepository _issueRepository;

        public IssueService(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository ?? throw new ArgumentNullException(nameof(issueRepository));
        }

        public async Task<IssueDescriptor> GetIssueDescriptor(Guid? id, CancellationToken cancellationToken)
        {
            if (!id.HasValue)
            {
                return null;
            }

            var issue = await _issueRepository.Get(id.Value, cancellationToken);
            return issue.Descriptor();
        }
    }
}