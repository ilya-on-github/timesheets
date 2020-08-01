using System;
using System.Threading;
using System.Threading.Tasks;

namespace Timesheets.Services.Commands.Employees
{
    public interface IEmployeeRepository
    {
        Task<Employee> Get(Guid id, CancellationToken cancellationToken);
        Task Save(Employee employee, CancellationToken cancellationToken);
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}