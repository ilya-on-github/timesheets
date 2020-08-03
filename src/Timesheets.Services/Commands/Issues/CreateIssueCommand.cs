using System;
using MediatR;
using Timesheets.Services.Queries.Issues;

namespace Timesheets.Services.Commands.Issues
{
    public class CreateIssueCommand : ICommand, IRequest<IIssue>
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