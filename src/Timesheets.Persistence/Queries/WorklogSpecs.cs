using System;
using System.Linq.Expressions;
using Timesheets.Persistence.Models;
using Timesheets.Services.Queries.Worklogs;

namespace Timesheets.Persistence.Queries
{
    public static class WorklogSpecs
    {
        public static Expression<Func<DbWorklog, bool>> ByFilter(WorklogFilter filter)
        {
            return ByQuery(filter?.Query);
        }

        public static Expression<Func<DbWorklog, bool>> ByQuery(string query)
        {
            return x => string.IsNullOrWhiteSpace(query) ||
                        x.WorkDescription.ToLower().Contains(query.ToLower());
        }

        public static Expression<Func<DbWorklog, bool>> ById(Guid id)
        {
            return x => x.Id.Equals(id);
        }
    }
}