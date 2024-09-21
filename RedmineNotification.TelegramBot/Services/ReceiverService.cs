using RedmineNotification.TelegramBot.Abstracts;
using RedmineNotification.TelegramBot.Handlers;
using RedmineNotification.TelegramBot.Interfaces;
using Telegram.Bot;

namespace RedmineNotification.TelegramBot.Services;

public class ReceiverService : BaseReceiverService<UpdateHandler>, IReceiverService
{
    public ReceiverService(
        ITelegramBotClient botClient,
        UpdateHandler updateHandler)
        : base(botClient, updateHandler)
    {
    }
}
