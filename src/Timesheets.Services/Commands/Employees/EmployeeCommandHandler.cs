using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timesheets.Services.Commands.Accounts;
using Timesheets.Services.Queries.Employees;

namespace Timesheets.Services.Commands.Employees
{
    // ReSharper disable once UnusedType.Global
    public class EmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, IEmployee>,
        IRequestHandler<UpdateEmployeeCommand, IEmployee>, IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        public async Task<IEmployee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee(request.Name);

            await _employeeRepository.Save(employee, cancellationToken);

            return employee;
        }

        public async Task<IEmployee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.Get(request.Id, cancellationToken);

            employee.Update(request.Name);

            await _employeeRepository.Save(employee, cancellationToken);

            return employee;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeeRepository.Delete(request.Id, cancellationToken);

            return Unit.Value;
        }
    }
}