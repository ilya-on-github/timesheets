using System;
using System.Threading;
using System.Threading.Tasks;

namespace Timesheets.Services.Commands.Issues
{
    public interface IIssueService
    {
        Task<IssueDescriptor> GetIssueDescriptor(Guid? id, CancellationToken cancellationToken);
    }
}