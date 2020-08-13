using Newtonsoft.Json;

namespace Timesheets.Jira.Models
{
    public class AccountCategory
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("key")] public string Key { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
    }
}