using Microsoft.Extensions.Options;
using RedmineNotification.Core.Models.Bot;
using RedmineNotification.TelegramBot.Abstracts;
using RedmineNotification.TelegramBot.Dialogs;
using RedmineNotification.TelegramBot.Models;
using RedmineNotification.TelegramBot.Services;
using Telegram.Bot;

namespace RedmineNotification.TelegramBot.Commands;

public class StartCommand : BaseCommand
{
    private readonly UserService _userService;
    private readonly ITelegramBotClient _botClient;
    private readonly Settings _settings;
    private readonly User _user;

    public StartCommand(ITelegramBotClient botClient, User user, UserService userService, Settings settings)
    {
        _userService = userService;
        _botClient = botClient;
        _settings = settings;
        _user = user;
    }

    public override async Task HandleMessageAsync(string message, CancellationToken token)
    {
        _user.CurrentDialog = new StartDialog(_botClient, _user, _userService, _settings);

        await _user.CurrentDialog.StartDialogAsync();
    }
}
