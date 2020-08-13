using System;
using Newtonsoft.Json;

namespace Timesheets.Jira.Models
{
    public class Worklog
    {
        [JsonProperty("tempoWorklogId")] public int TempoWorklogId { get; set; }
        [JsonProperty("comment")] public string Comment { get; set; }
        [JsonProperty("timeSpentSeconds")] public int TimeSpentSeconds { get; set; }
        [JsonProperty("issue")] public WorklogIssue Issue { get; set; }
        [JsonProperty("started")] public DateTime Started { get; set; }
        [JsonProperty("worker")] public string Worker { get; set; }

        // "timeSpent": "1h",
        // "attributes": {},   
        // "billableSeconds": 3600,
        // "originId": 54549,
        // "originTaskId": 48672,
        // "dateCreated": "2019-12-02 10:34:38.000",
        // "dateUpdated": "2019-12-02 10:34:38.000",
        // "updater": "e.egorov"
    }
}