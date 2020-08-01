using System;
using System.Linq.Expressions;
using Timesheets.Persistence.Models;
using Timesheets.Services.Queries.Employees;

namespace Timesheets.Persistence.Queries
{
    public class EmployeeSpecs
    {
        public static Expression<Func<DbEmployee, bool>> ByFilter(EmployeeFilter filter)
        {
            return ByQuery(filter?.Query);
        }

        public static Expression<Func<DbEmployee, bool>> ByQuery(string query)
        {
            return x => string.IsNullOrWhiteSpace(query) ||
                        x.Name.ToLower().Contains(query.ToLower());
        }
    }
}