using System;
using Timesheets.Services.Queries.Employees;
using Timesheets.Services.Queries.Issues;

namespace Timesheets.Services.Queries.Worklogs
{
    public interface IWorklog
    {
        Guid Id { get; }
        IEmployee Employee { get; }
        IIssue Issue { get; }
        DateTimeOffset Started { get; }
        TimeSpan TimeSpent { get; }
        string WorkDescription { get; }
    }
}