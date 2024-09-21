using RedmineNotification.TelegramBot.Abstracts;
using RedmineNotification.TelegramBot.Dialogs;
using RedmineNotification.TelegramBot.Models;
using RedmineNotification.TelegramBot.Services;
using Telegram.Bot;

namespace RedmineNotification.TelegramBot.Commands;

public class ChangeRedmineLoginCommand : BaseCommand
{
    private readonly UserService _userService;
    private readonly ITelegramBotClient _botClient;
    private readonly User _user;

    public ChangeRedmineLoginCommand(ITelegramBotClient botClient, User user, UserService userService)
    {
        _userService = userService;
        _botClient = botClient;
        _user = user;
    }

    public override async Task HandleMessageAsync(string message, CancellationToken token)
    {
        _user.CurrentDialog = new ChangeRedmineLoginDialog(_botClient, _user, _userService);

        await _user.CurrentDialog.StartDialogAsync();
    }
}
