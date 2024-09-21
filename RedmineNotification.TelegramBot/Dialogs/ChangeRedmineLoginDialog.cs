using RedmineNotification.TelegramBot.Abstracts;
using RedmineNotification.TelegramBot.Models;
using RedmineNotification.TelegramBot.Services;
using Telegram.Bot;

namespace RedmineNotification.TelegramBot.Dialogs;

public class ChangeRedmineLoginDialog : BaseDialog
{
    private readonly UserService _userService;
    private readonly ITelegramBotClient _botClient;
    private readonly User _user;

    private enum DialogState
    {
        Initial,
        Final,
    }

    private DialogState _currentSate = DialogState.Initial;

    public ChangeRedmineLoginDialog(ITelegramBotClient botClient, User user, UserService userService)
    {
        _userService = userService;
        _botClient = botClient;
        _user = user;
    }

    public override async Task StartDialogAsync()
    {
        await _botClient.SendTextMessageAsync(_user.ChatId, "Введите новый логин Redmine:");

        _currentSate = DialogState.Final;
    }

    public override async Task HandleMessageAsync(string message)
    {
        switch (_currentSate)
        {
            case DialogState.Final:
                _user.RedmineLogin = message;
                _userService.UpdateUser(_user);
                await _botClient.SendTextMessageAsync(_user.ChatId, "Логин обновлен");
                _currentSate = DialogState.Final;
                break;
        }
    }
}
