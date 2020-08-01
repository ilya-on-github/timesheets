using System;
using MediatR;

namespace Timesheets.Services.Commands.Accounts
{
    public class DeleteAccountCommand : ICommand, IRequest
    {
        public Guid Id { get; }

        public DeleteAccountCommand(Guid id)
        {
            Id = id;
        }
    }
}