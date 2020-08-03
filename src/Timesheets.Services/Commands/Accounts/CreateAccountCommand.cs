using System;
using MediatR;

namespace Timesheets.Services.Commands.Accounts
{
    public class CreateAccountCommand : ICommand, IRequest<Guid>
    {
        public string Name { get; }

        public CreateAccountCommand(string name)
        {
            Name = name;
        }
    }
}