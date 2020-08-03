using System;
using MediatR;

namespace Timesheets.Services.Commands.Issues
{
    public class CreateIssueCommand : ICommand, IRequest<Guid>
    {
        public string Summary { get; }
        public string Description { get; }
        public Guid? AccountId { get; }

        public CreateIssueCommand(string summary, string description, Guid? accountId)
        {
            Summary = summary;
            Description = description;
            AccountId = accountId;
        }
    }
}