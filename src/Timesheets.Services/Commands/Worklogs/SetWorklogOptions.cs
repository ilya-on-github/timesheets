using System;

namespace Timesheets.Services.Commands.Worklogs
{
    public class SetWorklogOptions
    {
        public Guid? EmployeeId { get; set; }
        public Guid? IssueId { get; set; }
        public DateTimeOffset? Started { get; set; }
        public TimeSpan? TimeSpent { get; set; }
        public string WorkDescription { get; set; }
    }
}