using System;
using MediatR;

namespace Timesheets.Services.Commands.Employees
{
    public class UpdateEmployeeCommand : ICommand, IRequest
    {
        public Guid Id { get; }
        public string Name { get; }

        public UpdateEmployeeCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}