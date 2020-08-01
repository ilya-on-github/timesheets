using System;
using MediatR;
using Timesheets.Services.Queries.Employees;

namespace Timesheets.Services.Commands.Employees
{
    public class UpdateEmployeeCommand : ICommand, IRequest<IEmployee>
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