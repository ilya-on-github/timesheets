using System;
using System.Linq.Expressions;
using Timesheets.Persistence.Models;
using Timesheets.Services.Queries.Issues;

namespace Timesheets.Persistence.Queries
{
    public static class IssueSpecs
    {
        public static Expression<Func<DbIssue, bool>> ByFilter(IssueFilter filter)
        {
            return ByQuery(filter?.Query);
        }

        public static Expression<Func<DbIssue, bool>> ByQuery(string query)
        {
            return x => string.IsNullOrWhiteSpace(query) ||
                        x.Summary.ToLower().Contains(query.ToLower());
        }

        public static Expression<Func<DbIssue, bool>> ById(Guid id)
        {
            return x => x.Id.Equals(id);
        }
    }
}