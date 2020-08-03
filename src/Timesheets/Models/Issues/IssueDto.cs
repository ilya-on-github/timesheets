using System;
using Timesheets.Models.Accounts;

namespace Timesheets.Models.Issues
{
    public class IssueDto
    {
        public Guid Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public AccountDto Account { get; set; }
    }
}