namespace RedmineNotification.TelegramBot.Abstracts;

public abstract class BaseCommand
{
    public abstract Task HandleMessageAsync(string message, CancellationToken token);
}
