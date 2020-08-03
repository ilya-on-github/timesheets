using System;
using MediatR;

namespace Timesheets.Services.Commands.Issues
{
    public class DeleteIssueCommand : ICommand, IRequest
    {
        public Guid Id { get; }

        public DeleteIssueCommand(Guid id)
        {
            Id = id;
        }
    }
}