using Microsoft.AspNetCore.Mvc;
using RedmineNotification.Api.Services;

namespace RedmineNotification.Api.Controllers;

[ApiController]
[Route("/")]
public class WebhookController : ControllerBase
{
    private readonly NotificationService _notificationService;
    private readonly HttpContextService _contextService;
    private readonly ILogger<WebhookController> _logger;

    public WebhookController(HttpContextService contextService, NotificationService notificationService, ILogger<WebhookController> logger)
    {
        _contextService = contextService;
        _notificationService = notificationService;
        _logger = logger;
    }

    [HttpPost("webhook")]
    public async Task Hook(CancellationToken ctn)
    {
        try
        {
            var payload = await _contextService.ReturnBodyAsync(HttpContext);

            await _notificationService.SendNotificationAsync(payload, ctn);

            _logger.LogInformation($"Received webhook with action: {payload.Payload?.Action}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when procceed webhook");
        }
    }
}
