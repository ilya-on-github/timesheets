using System;
using MediatR;
using Timesheets.Services.Commands.Accounts;

namespace Timesheets.Services.Commands.Employees
{
    public class DeleteEmployeeCommand : ICommand, IRequest
    {
        public Guid Id { get; }

        public DeleteEmployeeCommand(Guid id)
        {
            Id = id;
        }
    }
}