using Microsoft.Extensions.Options;
using RedmineNotification.Core.Exceptions;
using RedmineNotification.Core.Models.Bot;
using RedmineNotification.TelegramBot.Abstracts;
using RedmineNotification.TelegramBot.Commands;
using RedmineNotification.TelegramBot.Services;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using User = RedmineNotification.TelegramBot.Models.User;

namespace RedmineNotification.TelegramBot.Handlers;

public class UpdateHandler : IUpdateHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly UserService _userService;
    private readonly Settings _settings;

    public UpdateHandler(ITelegramBotClient botClient, UserService userService, IOptionsMonitor<Settings> settings)
    {
        _userService = userService;
        _botClient = botClient;
        _settings = settings.CurrentValue;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        var handler = update switch
        {
            { Message: { } message } => BotOnMessageReceived(message, cancellationToken),
            _ => UnknownUpdateHandlerAsync(update, cancellationToken)
        };

        await handler;
    }

    public async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException =>
                $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        if (exception is RequestException)
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
    }

    private async Task BotOnMessageReceived(Message message, CancellationToken cancellationToken)
    {
        if (message.Text is not { } messageText)
            return;

        if (!_userService.UserExists(message.Chat.Id))
            _userService.AddUser(new User
            {
                ChatId = message.Chat.Id,
            });

        var currentUser = _userService.GetUser(message.Chat.Id);

        if (currentUser is null)
        {
            throw new SimpleException($"User with {message.Chat.Id} not foud");
        }

        if (messageText.StartsWith("/start"))
        {
            BaseCommand command = new StartCommand(_botClient, currentUser, _userService, _settings);
            await command.HandleMessageAsync(messageText, cancellationToken);
        }
        else if (messageText.StartsWith("/change_redmine_login") && !messageText.StartsWith("/start"))
        {
            if (!currentUser.IsAuth)
                return;

            BaseCommand command = new ChangeRedmineLoginCommand(_botClient, currentUser, _userService);
            await command.HandleMessageAsync(messageText, cancellationToken);
        }
        else
        {
            var dialog = currentUser.CurrentDialog;

            if (dialog is null)
            {
                throw new SimpleException("Current dialog is null");
            }

            await dialog.HandleMessageAsync(messageText);
        }
    }

    private Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
