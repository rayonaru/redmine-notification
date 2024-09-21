using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RedmineNotification.Core.Models.Bot;
using Telegram.Bot.Types.Enums;

namespace RedmineNotification.Api.Services;

public class MessageService
{
    private readonly string _url;
    private readonly HttpClient _httpClient;
    
    public MessageService(HttpClient httpClient, IOptionsMonitor<Settings> settings)
    {
        _httpClient = httpClient;
        _url = $"https://api.telegram.org/bot{settings.CurrentValue.BOT_TOKEN}/sendMessage";
    }
    
    public async Task SendMessageAsync(long chatId, string message, CancellationToken ctn)
    {
        var content = new StringContent(JsonConvert.SerializeObject(new
        {
            chat_id = chatId,
            text = message,
            parse_mode = ParseMode.Markdown,
        }), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_url, content, ctn);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Failed to send message. Status Code: {response.StatusCode}");
        }
    } 
}
