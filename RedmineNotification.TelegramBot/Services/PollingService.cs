using Microsoft.Extensions.Logging;

namespace RedmineNotification.TelegramBot.Services;

public class PollingService : BasePollingService<ReceiverService>
{
    public PollingService(IServiceProvider serviceProvider, ILogger<PollingService> logger)
        : base(serviceProvider)
    {
    } 
}
