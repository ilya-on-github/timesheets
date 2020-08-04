using System;
using System.Threading;
using System.Threading.Tasks;

namespace Timesheets.Services.Commands.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        public async Task<EmployeeDescriptor> GetEmployeeDescriptor(Guid? id, CancellationToken cancellationToken)
        {
            if (!id.HasValue)
            {
                return null;
            }

            var employee = await _employeeRepository.Get(id.Value, cancellationToken);
            return employee.Descriptor();
        }
    }
}