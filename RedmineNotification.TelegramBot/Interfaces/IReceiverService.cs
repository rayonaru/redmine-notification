namespace RedmineNotification.TelegramBot.Interfaces;

public interface IReceiverService
{
    Task ReceiveAsync(CancellationToken stoppingToken);
}
