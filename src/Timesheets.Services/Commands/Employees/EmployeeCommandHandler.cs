using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Timesheets.Services.Commands.Employees
{
    // ReSharper disable once UnusedType.Global
    public class EmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>,
        IRequestHandler<UpdateEmployeeCommand>, IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee(request.Name);

            await _employeeRepository.Save(employee, cancellationToken);

            return employee.Id;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.Get(request.Id, cancellationToken);

            employee.Update(request.Name);

            await _employeeRepository.Save(employee, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeeRepository.Delete(request.Id, cancellationToken);

            return Unit.Value;
        }
    }
}