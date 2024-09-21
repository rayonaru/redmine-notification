using Newtonsoft.Json;

namespace RedmineNotification.Core.Models.Payload;

public class Message
{
    [JsonProperty("payload")]
    public Payload? Payload { get; set; }
}
