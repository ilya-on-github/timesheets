using System;
using Timesheets.Services.Queries.Accounts;

namespace Timesheets.Services.Queries.Issues
{
    public interface IIssue
    {
        Guid Id { get; }
        string Summary { get; }
        string Description { get; }
        IAccount Account { get; }
    }
}