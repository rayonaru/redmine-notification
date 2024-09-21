using Newtonsoft.Json;

namespace RedmineNotification.Core.Models.Payload;

public class Priority
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }
}
