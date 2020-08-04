using System;
using Timesheets.Models.Employees;
using Timesheets.Models.Issues;

namespace Timesheets.Models.Worklogs
{
    public class CreateWorklogDto
    {
        public EmployeeRefDto Employee { get; set; }
        public IssueRefDto Issue { get; set; }
        public DateTimeOffset? Started { get; set; }
        public double? TimeSpentSeconds { get; set; }
        public string WorkDescription { get; set; }
    }
}