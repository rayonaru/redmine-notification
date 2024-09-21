using Newtonsoft.Json;

namespace RedmineNotification.Core.Models.Payload;

public class Author
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("login")]
    public string? Login { get; set; }

    [JsonProperty("mail")]
    public string? Mail { get; set; }

    [JsonProperty("firstname")]
    public string? Firstname { get; set; }

    [JsonProperty("lastname")]
    public string? Lastname { get; set; }

    [JsonProperty("identity_url")]
    public object? IdentityUrl { get; set; }

    [JsonProperty("icon_url")]
    public string? IconUrl { get; set; } 
}
