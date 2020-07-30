using System.Collections.Generic;

namespace Timesheets.Services.Queries.Employees
{
    public interface IEmployeeQuery : IQuery<EmployeeFilter, IEnumerable<IEmployee>>
    {
    }
}