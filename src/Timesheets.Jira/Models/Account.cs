using Newtonsoft.Json;

namespace Timesheets.Jira.Models
{
    public class Account
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("key")] public string Key { get; set; }
        [JsonProperty("name")] public string Name { get; set; }

        // lead
        // leadAvatar
        // contactAvatar
        // status

        [JsonProperty("category")] public AccountCategory Category { get; set; }

        // global
    }
}