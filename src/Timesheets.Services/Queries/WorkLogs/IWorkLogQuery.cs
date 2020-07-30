using System;
using System.Collections.Generic;
using MediatR;

namespace Timesheets.Services.Queries.WorkLogs
{
    public class WorkLogQuery : IRequest<IEnumerable<IWorkLog>>
    {
        public WorkLogQuery(WorkLogFilter filter)
        {
            Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }

        public WorkLogFilter Filter { get; }
    }
}