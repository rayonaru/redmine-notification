using Newtonsoft.Json;

namespace RedmineNotification.Core.Models.Payload;

public class Issue
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("subject")]
    public string? Subject { get; set; }

    [JsonProperty("description")]
    public string? Description { get; set; }

    [JsonProperty("created_on")]
    public DateTime CreatedOn { get; set; }

    [JsonProperty("updated_on")]
    public DateTime UpdatedOn { get; set; }

    [JsonProperty("closed_on")]
    public object? ClosedOn { get; set; }

    [JsonProperty("root_id")]
    public int RootId { get; set; }

    [JsonProperty("parent_id")]
    public object? ParentId { get; set; }

    [JsonProperty("done_ratio")]
    public int DoneRatio { get; set; }

    [JsonProperty("start_date")]
    public string? StartDate { get; set; }

    [JsonProperty("due_date")]
    public object? DueDate { get; set; }

    [JsonProperty("estimated_hours")]
    public object? EstimatedHours { get; set; }

    [JsonProperty("is_private")]
    public bool IsPrivate { get; set; }

    [JsonProperty("lock_version")]
    public int LockVersion { get; set; }

    [JsonProperty("custom_field_values")]
    public List<object>? CustomFieldValues { get; set; }

    [JsonProperty("project")]
    public Project? Project { get; set; }

    [JsonProperty("status")]
    public Status? Status { get; set; }

    [JsonProperty("tracker")]
    public Tracker? Tracker { get; set; }

    [JsonProperty("priority")]
    public Priority? Priority { get; set; }

    [JsonProperty("author")]
    public Author? Author { get; set; }

    [JsonProperty("assignee")]
    public Assignee? Assignee { get; set; }

    [JsonProperty("watchers")]
    public List<Watcher>? Watchers { get; set; }
}
