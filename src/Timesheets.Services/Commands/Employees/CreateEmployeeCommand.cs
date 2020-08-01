using MediatR;
using Timesheets.Services.Queries.Employees;

namespace Timesheets.Services.Commands.Employees
{
    public class CreateEmployeeCommand : ICommand, IRequest<IEmployee>
    {
        public string Name { get; }

        public CreateEmployeeCommand(string name)
        {
            Name = name;
        }
    }
}