using System;

namespace Timesheets.Jira.Persistence.Models
{
    public class DbExternalEntity
    {
        public DateTimeOffset Checked { get; set; }
        public DateTimeOffset Updated { get; set; }
        public bool IsRemoved { get; set; }
    }
}