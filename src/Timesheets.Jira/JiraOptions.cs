using System;

namespace Timesheets.Jira
{
    public class JiraOptions
    {
        public Uri BaseUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public TimeSpan TimeOffset { get; set; }
    }
}