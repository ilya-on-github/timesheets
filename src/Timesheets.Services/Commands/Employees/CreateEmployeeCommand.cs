using System;
using MediatR;

namespace Timesheets.Services.Commands.Employees
{
    public class CreateEmployeeCommand : ICommand, IRequest<Guid>
    {
        public string Name { get; }

        public CreateEmployeeCommand(string name)
        {
            Name = name;
        }
    }
}