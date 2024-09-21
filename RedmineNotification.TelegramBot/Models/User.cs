using Newtonsoft.Json;
using RedmineNotification.TelegramBot.Abstracts;

namespace RedmineNotification.TelegramBot.Models;

public class User
{
    public long ChatId { get; init; }
    [JsonIgnore]
    public BaseDialog? CurrentDialog { get; set; }
    public bool IsAuth { get; set; }
    public string? RedmineLogin { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj is User otherUser)
        {
            return RedmineLogin == otherUser.RedmineLogin;
        }
        
        return false;
    }

    public override int GetHashCode()
    {
        return RedmineLogin != null ? RedmineLogin.GetHashCode() : 0;
    }
}
