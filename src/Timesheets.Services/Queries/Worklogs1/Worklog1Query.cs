using System;
using System.Collections.Generic;
using MediatR;

namespace Timesheets.Services.Queries.Worklogs1
{
    public class Worklog1Query : IRequest<IEnumerable<IWorklog1>>
    {
        public Worklog1Query(Worklog1Filter filter)
        {
            Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }

        public Worklog1Filter Filter { get; }
    }
}