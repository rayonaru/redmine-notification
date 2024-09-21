using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace RedmineNotification.TelegramBot.Abstracts;

public abstract class BaseReceiverService<TUpdateHandler>
    where TUpdateHandler : IUpdateHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUpdateHandler _updateHandler;

    private protected BaseReceiverService(
        ITelegramBotClient botClient,
        TUpdateHandler updateHandler)
    {
        _botClient = botClient;
        _updateHandler = updateHandler;
    }

    public async Task ReceiveAsync(CancellationToken stoppingToken)
    {
        // ToDo: we can inject ReceiverOptions through IOptions container
        var receiverOptions = new ReceiverOptions()
        {
            AllowedUpdates = Array.Empty<UpdateType>(),
            ThrowPendingUpdates = true,
        };

        // var me = await _botClient.GetMeAsync(stoppingToken);

        // Start receiving updates
        await _botClient.ReceiveAsync(
            updateHandler: _updateHandler,
            receiverOptions: receiverOptions,
            cancellationToken: stoppingToken);
    }
}
