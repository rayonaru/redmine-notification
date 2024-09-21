using System.Text;
using Newtonsoft.Json;
using RedmineNotification.Core.Exceptions;
using RedmineNotification.Core.Models.Payload;

namespace RedmineNotification.Api.Services;

public class HttpContextService
{
    public async Task<Message> ReturnBodyAsync(HttpContext context)
    {
        using var reader = new StreamReader(context.Request.Body, Encoding.UTF8);
        var body = await reader.ReadToEndAsync();
        
        if (body is null)
        {
            throw new SimpleException("Empty body");
        }
        
        var payload = JsonConvert.DeserializeObject<Message>(body);

        if (payload is null)
        {
            throw new SimpleException("Empty payload");
        }

        return payload;
    }
}
