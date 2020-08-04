using System;
using Timesheets.Models.Employees;
using Timesheets.Models.Issues;

namespace Timesheets.Models.Worklogs
{
    public class WorklogDto
    {
        public Guid Id { get; set; }
        public EmployeeDto Employee { get; set; }
        public IssueDto Issue { get; set; }
        public DateTimeOffset Started { get; set; }
        public double TimeSpentSeconds { get; set; }
        public string WorkDescription { get; set; }
    }
}