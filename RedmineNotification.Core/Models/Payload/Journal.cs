using Newtonsoft.Json;

namespace RedmineNotification.Core.Models.Payload;

public class Journal
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("notes")]
    public string? Notes { get; set; }

    [JsonProperty("created_on")]
    public DateTime CreatedOn { get; set; }

    [JsonProperty("private_notes")]
    public bool PrivateNotes { get; set; }

    [JsonProperty("author")]
    public Author? Author { get; set; }

    [JsonProperty("details")]
    public List<Detail>? Details { get; set; } 
}
