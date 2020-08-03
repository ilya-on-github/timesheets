using System;
using MediatR;

namespace Timesheets.Services.Commands.Issues
{
    public class DeleteIssueCommand : ICommand, IRequest<Unit>
    {
        public Guid Id { get; }

        public DeleteIssueCommand(Guid id)
        {
            Id = id;
        }
    }
}