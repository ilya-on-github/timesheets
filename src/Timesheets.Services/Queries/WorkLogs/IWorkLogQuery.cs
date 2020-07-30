using System.Collections.Generic;

namespace Timesheets.Services.Queries.WorkLogs
{
    public interface IWorkLogQuery : IQuery<WorkLogFilter, IEnumerable<IWorkLog>>
    {
    }
}