using RedmineNotification.Core.Models.Bot;
using RedmineNotification.TelegramBot.Abstracts;
using RedmineNotification.TelegramBot.Models;
using RedmineNotification.TelegramBot.Services;
using Telegram.Bot;

namespace RedmineNotification.TelegramBot.Dialogs;

public class StartDialog : BaseDialog
{
    private readonly UserService _userService;
    private readonly ITelegramBotClient _botClient;
    private readonly Settings _settings;
    private readonly User _user;

    private enum DialogState
    {
        Initial,
        TokenEntered,
        RedmineLoginEntered,
        Final,
    }

    private DialogState _currentSate = DialogState.Initial;

    public StartDialog(ITelegramBotClient botClient, User user, UserService userService, Settings settings)
    {
        _userService = userService;
        _botClient = botClient;
        _settings = settings;
        _user = user;
    }

    public override async Task StartDialogAsync()
    {
        if (_user.IsAuth)
        {
            _currentSate = DialogState.Final;
            await _botClient.SendTextMessageAsync(_user.ChatId, "Я уже запущен");
        }
        else
        {
            await _botClient.SendTextMessageAsync(_user.ChatId, "Добро пожаловать");
            await _botClient.SendTextMessageAsync(_user.ChatId, "Введи токен доступа:");
            _currentSate = DialogState.TokenEntered;
        }
    }

    public override async Task HandleMessageAsync(string message)
    {
        switch (_currentSate)
        {
            case DialogState.TokenEntered:
                if (message.Equals(_settings.SECRET_TOKEN))
                {
                    _user.IsAuth = true;
                    await _botClient.SendTextMessageAsync(_user.ChatId, "Введи логин из Redmine:");
                    _currentSate = DialogState.RedmineLoginEntered;
                }
                else
                {
                    await _botClient.SendTextMessageAsync(_user.ChatId,
                        "Неверный токен.\nДля получения доступа пишите сюда: @rayonaru\nВведи корректный токен доступа:");
                }

                break;
            case DialogState.RedmineLoginEntered:
                await _botClient.SendTextMessageAsync(_user.ChatId,
                    "Доступ предоставлен. Теперь сюда будут приходить уведомления об изменениях задач в Redmine.\nПо всем возникшим вопросам писать сюда: @rayonaru");
                _user.RedmineLogin = message;
                await _botClient.SendTextMessageAsync(_user.ChatId, "Хорошего пользования!");
                _userService.UpdateUser(_user);
                _currentSate = DialogState.Final;
                break;
            case DialogState.Final:
                await _botClient.SendTextMessageAsync(_user.ChatId, "Доступ уже получен");
                break;
            default:
                await StartDialogAsync();
                break;
        }
    }
}
