using System;
using MediatR;
using Timesheets.Services.Queries.Issues;

namespace Timesheets.Services.Commands.Issues
{
    public class UpdateIssueCommand : ICommand, IRequest
    {
        public Guid Id { get; }
        public string Summary { get; }
        public string Description { get; }
        public Guid? AccountId { get; }

        public UpdateIssueCommand(Guid id, string summary, string description, Guid? accountId)
        {
            Id = id;
            Summary = summary;
            Description = description;
            AccountId = accountId;
        }
    }
}