using System;
using System.Threading;
using System.Threading.Tasks;

namespace Timesheets.Services.Commands.Issues
{
    public interface IAccountService
    {
        Task<AccountDescriptor> GetAccountDescriptor(Guid? id, CancellationToken cancellationToken);
    }
}