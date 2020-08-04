using System;
using Timesheets.Services.Commands.Employees;
using Timesheets.Services.Commands.Issues;

namespace Timesheets.Services.Commands.Worklogs
{
    public class WorklogOptions
    {
        public EmployeeDescriptor Employee { get; set; }
        public IssueDescriptor Issue { get; set; }
        public DateTimeOffset? Started { get; set; }
        public TimeSpan? TimeSpent { get; set; }
        public string WorkDescription { get; set; }
    }
}