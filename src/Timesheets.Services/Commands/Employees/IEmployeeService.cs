using System;
using System.Threading;
using System.Threading.Tasks;

namespace Timesheets.Services.Commands.Employees
{
    public interface IEmployeeService
    {
        Task<EmployeeDescriptor> GetEmployeeDescriptor(Guid? id, CancellationToken cancellationToken);
    }
}