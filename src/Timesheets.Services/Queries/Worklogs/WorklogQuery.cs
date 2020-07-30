using System;
using System.Collections.Generic;
using MediatR;

namespace Timesheets.Services.Queries.Worklogs
{
    public class WorklogQuery : IRequest<IEnumerable<IWorklog>>
    {
        public WorklogQuery(WorklogFilter filter)
        {
            Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }

        public WorklogFilter Filter { get; }
    }
}