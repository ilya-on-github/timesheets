using System;
using MediatR;
using Timesheets.Services.Queries.Accounts;

namespace Timesheets.Services.Commands.Accounts
{
    public class UpdateAccountCommand : ICommand, IRequest<IAccount>
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