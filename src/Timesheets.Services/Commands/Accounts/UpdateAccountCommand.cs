using System;
using MediatR;

namespace Timesheets.Services.Commands.Accounts
{
    public class UpdateAccountCommand : ICommand, IRequest
    {
        public Guid Id { get; }
        public string Name { get; }

        public UpdateAccountCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}