using System;
using Timesheets.Services.Commands.Employees;
using Timesheets.Services.Commands.Issues;

namespace Timesheets.Services.Commands.Worklogs
{
    public class Worklog
    {
        public Guid Id { get; }
        public Guid EmployeeId { get; private set; }
        public Guid IssueId { get; private set; }
        public DateTimeOffset Started { get; private set; }
        public TimeSpan TimeSpent { get; private set; }
        public string WorkDescription { get; private set; }

        public Worklog(WorklogOptions options)
        {
            Id = Guid.NewGuid();

            SetEmployee(options.Employee);
            SetIssue(options.Issue);
            SetStarted(options.Started);
            SetTimeSpent(options.TimeSpent);
            SetWorkDescription(options.WorkDescription);
        }

        public Worklog(Guid id, Guid employeeId, Guid issueId, DateTimeOffset started, TimeSpan timeSpent,
            string workDescription)
        {
            Id = id;
            EmployeeId = employeeId;
            IssueId = issueId;
            Started = started;
            TimeSpent = timeSpent;
            WorkDescription = workDescription;
        }

        public void Update(WorklogOptions options)
        {
            SetEmployee(options.Employee);
            SetIssue(options.Issue);
            SetStarted(options.Started);
            SetTimeSpent(options.TimeSpent);
            SetWorkDescription(options.WorkDescription);
        }

        private void SetEmployee(EmployeeDescriptor employee)
        {
            if (employee == null)
            {
                throw new ArgumentException("Employee have to be specified.");
            }

            EmployeeId = employee.Id;
        }

        private void SetIssue(IssueDescriptor issue)
        {
            if (issue == null)
            {
                throw new ArgumentException("Issue have to be specified.");
            }

            IssueId = issue.Id;
        }

        private void SetStarted(DateTimeOffset? started)
        {
            if (!started.HasValue)
            {
                throw new ArgumentException("'Started' value have to be specified.");
            }

            Started = started.Value;
        }

        private void SetTimeSpent(TimeSpan? timeSpent)
        {
            if (!timeSpent.HasValue)
            {
                throw new ArgumentException("'TimeSpent' value have to be specified.");
            }

            TimeSpent = timeSpent.Value;
        }

        private void SetWorkDescription(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Work description can't be empty.");
            }

            WorkDescription = input.Trim().RemoveDoubleSpaces();
        }
    }
}