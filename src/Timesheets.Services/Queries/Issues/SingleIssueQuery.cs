using System;
using MediatR;

namespace Timesheets.Services.Queries.Issues
{
    public class SingleIssueQuery : IRequest<IIssue>
    {
        public Guid Id { get; }

        public SingleIssueQuery(Guid id)
        {
            Id = id;
        }
    }
}