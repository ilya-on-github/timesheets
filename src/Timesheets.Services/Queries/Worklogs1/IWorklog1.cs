using System;
using Timesheets.Services.Queries.Employees;

namespace Timesheets.Services.Queries.Worklogs1
{
    public interface IWorklog1
    {
        Guid Id { get; }
        IEmployee Employee { get; }
        IIssue Issue { get; }
        DateTimeOffset Started { get; }
        TimeSpan TimeSpent { get; }
        string WorkDescription { get; }
    }
}