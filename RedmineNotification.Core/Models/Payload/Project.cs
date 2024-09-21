using Newtonsoft.Json;

namespace RedmineNotification.Core.Models.Payload;

public class Project
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("identifier")]
    public string? Identifier { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("description")]
    public string? Description { get; set; }

    [JsonProperty("created_on")]
    public DateTime? CreatedOn { get; set; }

    [JsonProperty("homepage")]
    public string? Homepage { get; set; }
}
