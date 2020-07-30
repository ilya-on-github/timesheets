using System.Collections.Generic;

namespace Timesheets.Services.Queries.Accounts
{
    public interface IAccountQuery : IQuery<AccountFilter, IEnumerable<IAccount>>
    {
    }
}