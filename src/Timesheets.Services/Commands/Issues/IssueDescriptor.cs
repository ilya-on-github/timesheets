using System;

namespace Timesheets.Services.Commands.Issues
{
    public class IssueDescriptor
    {
        public Guid Id { get; }
        public string Summary { get; }

        public IssueDescriptor(Guid id, string summary)
        {
            Id = id;
            Summary = summary;
        }
    }
}