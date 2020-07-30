using System;

namespace Timesheets.Services.Queries.Accounts
{
    public interface IAccount
    {
        Guid Id { get; }
        string Name { get; }
    }
}