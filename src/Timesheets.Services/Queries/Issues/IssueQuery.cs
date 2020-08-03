using System.Collections.Generic;
using MediatR;

namespace Timesheets.Services.Queries.Issues
{
    public class IssueQuery : IRequest<IEnumerable<IIssue>>
    {
        public IssueQuery(IssueFilter filter)
        {
            Filter = filter;
        }

        public IssueFilter Filter { get; }
    }
}