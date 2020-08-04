using System;
using MediatR;

namespace Timesheets.Services.Commands.Worklogs
{
    public class DeleteWorklogCommand : IRequest
    {
        public Guid Id { get; }

        public DeleteWorklogCommand(Guid id)
        {
            Id = id;
        }
    }
}