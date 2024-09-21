using Newtonsoft.Json;

namespace RedmineNotification.Core.Models.Payload;

public class Detail
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("value")]
    public string? Value { get; set; }

    [JsonProperty("old_value")]
    public string? OldValue { get; set; }

    [JsonProperty("prop_key")]
    public string? PropKey { get; set; }

    [JsonProperty("property")]
    public string? Property { get; set; } 
}
