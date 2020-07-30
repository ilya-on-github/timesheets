using System;
using Timesheets.Services.Queries.Employees;

namespace Timesheets.Services.Queries.WorkLogs
{
    public interface IWorkLog
    {
        Guid Id { get; }
        IEmployee Employee { get; }
        IIssue Issue { get; }
        DateTimeOffset Started { get; }
        TimeSpan TimeSpent { get; }
        string WorkDescription { get; }
    }
}