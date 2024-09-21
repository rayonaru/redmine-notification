namespace RedmineNotification.TelegramBot.Abstracts;

public abstract class BaseDialog
{
    public abstract Task HandleMessageAsync(string message);
    public abstract Task StartDialogAsync();
}
