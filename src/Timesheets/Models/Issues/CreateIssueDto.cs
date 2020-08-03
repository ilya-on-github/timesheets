using Timesheets.Models.Accounts;

namespace Timesheets.Models.Issues
{
    public class CreateIssueDto
    {
        public string Summary { get; set; }
        public string Description { get; set; }
        public AccountRefDto Account { get; set; }
    }
}