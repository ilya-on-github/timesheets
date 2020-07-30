using System;
using System.Linq.Expressions;
using Timesheets.Persistence.Models;
using Timesheets.Services.Queries.Accounts;

namespace Timesheets.Persistence.Queries
{
    public static class AccountSpecs
    {
        public static Expression<Func<Account, bool>> ByFilter(AccountFilter filter)
        {
            return ByQuery(filter?.Query);
        }

        public static Expression<Func<Account, bool>> ByQuery(string query)
        {
            return x => string.IsNullOrWhiteSpace(query) ||
                        x.Name.ToLower().Contains(query.ToLower());
        }
    }
}