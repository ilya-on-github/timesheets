using System;
using System.Threading;
using System.Threading.Tasks;

namespace Timesheets.Services.Commands.Worklogs
{
    public interface IWorklogRepository
    {
        Task<Worklog> Get(Guid id, CancellationToken cancellationToken);
        Task Save(Worklog worklog, CancellationToken cancellationToken);
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}