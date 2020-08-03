using System;
using System.Threading;
using System.Threading.Tasks;

namespace Timesheets.Services.Commands.Issues
{
    public interface IIssueRepository
    {
        Task<Issue> Get(Guid id, CancellationToken cancellationToken);
        Task Save(Issue issue, CancellationToken cancellationToken);
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}