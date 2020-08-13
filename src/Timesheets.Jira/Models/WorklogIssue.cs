using Newtonsoft.Json;

namespace Timesheets.Jira.Models
{
    public class WorklogIssue
    {
        [JsonProperty("key")] public string Key { get; set; }
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("summary")] public string Summary { get; set; }
        [JsonProperty("accountKey")] public string AccountKey { get; set; }

        // "components": [],
        // "iconUrl": "/secure/viewavatar?size=xsmall&avatarId=10315&avatarType=issuetype",
        // "versions": [],
        // "projectKey": "ES",
        // "projectId": 12103,
        // "issueType": "История",
        // "issueStatus": "To Do",
        // "epicKey": "ES-76",
        // "internalIssue": false,
        // "estimatedRemainingSeconds": 0,
        // "reporterKey": "v.fedorko",
    }
}