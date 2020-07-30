using System;
using System.Collections.Generic;
using MediatR;

namespace Timesheets.Services.Queries.Accounts
{
    public class AccountQuery : IRequest<IEnumerable<IAccount>>
    {
        public AccountQuery(AccountFilter filter)
        {
            Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }

        public AccountFilter Filter { get; }
    }
}