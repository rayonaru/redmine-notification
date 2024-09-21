using Newtonsoft.Json;

namespace RedmineNotification.Core.Models.Payload;

public class Payload
{
    [JsonProperty("action")]
    public string? Action { get; set; }

    [JsonProperty("issue")]
    public Issue? Issue { get; set; }

    [JsonProperty("journal")]
    public Journal? Journal { get; set; }

    [JsonProperty("url")]
    public string? Url { get; set; }
}
