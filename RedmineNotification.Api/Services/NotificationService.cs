using RedmineNotification.Api.Helpers;
using RedmineNotification.Core.Models.Payload;
using RedmineNotification.TelegramBot.Models;
using RedmineNotification.TelegramBot.Services;

namespace RedmineNotification.Api.Services;

public class NotificationService
{
    private readonly MessageService _messageService;
    private readonly UserService _userService;

    public NotificationService(MessageService messageService, UserService userService)
    {
        _messageService = messageService;
        _userService = userService;
    }

    public async Task SendNotificationAsync(Message message, CancellationToken ctn)
    {
        var users = GetUsersForNotification(message?.Payload?.Issue);

        var sendMessageTasks = new List<Task>();

        var formattedMessage = MessageFormatHelper.FormatPayloadMessage(message?.Payload);

        foreach (var user in users)
        {
            var sendMessageTask = _messageService.SendMessageAsync(user.ChatId, formattedMessage, ctn);
            sendMessageTasks.Add(sendMessageTask);
        }

        await Task.WhenAll(sendMessageTasks);
    }

    private IEnumerable<User> GetUsersForNotification(Issue? issue)
    {
        if (issue is null)
        {
            return Enumerable.Empty<User>();
        }

        var userLogins = new HashSet<string>();

        if (!string.IsNullOrEmpty(issue.Assignee?.Login))
        {
            userLogins.Add(issue.Assignee.Login);
        }

        if (!string.IsNullOrEmpty(issue.Author?.Login))
        {
            userLogins.Add(issue.Author.Login);
        }

        if (issue.Watchers != null)
        {
            foreach (var watcher in issue!.Watchers.Where(watcher => !string.IsNullOrEmpty(watcher.Login)))
            {
                userLogins.Add(watcher!.Login!);
            }
        }

        var users = _userService.GetUsers(userLogins.ToArray());

        return users;
    }
}
