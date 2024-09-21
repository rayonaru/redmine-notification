using dotenv.net;
using RedmineNotification.Api.Services;
using RedmineNotification.Core.Exceptions;
using RedmineNotification.Core.Models.Bot;
using RedmineNotification.TelegramBot.Handlers;
using RedmineNotification.TelegramBot.Services;
using Telegram.Bot;

DotEnv.Load(new DotEnvOptions(envFilePaths: new[] { "../config/.env" }, ignoreExceptions: true));

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
builder.Services.Configure<Settings>(configuration.GetSection("SETTINGS"));

var telegramBotToken = Environment.GetEnvironmentVariable("SETTINGS__BOT_TOKEN");
var usersJsonPath = Environment.GetEnvironmentVariable("SETTINGS__USERS_JSON_PATH");

builder.Services.AddHttpClient();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<HttpContextService>();
builder.Services.AddScoped<UpdateHandler>();
builder.Services.AddScoped<ReceiverService>();
builder.Services.AddSingleton(_ => new UserService(usersJsonPath));

builder.Services.AddHostedService<PollingService>();

builder.Services.AddHttpClient("telegram_bot_client")
    .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
    {
        TelegramBotClientOptions options = new(telegramBotToken ?? throw new SimpleException("Telefram bot token is null"));
        return new TelegramBotClient(options, httpClient);
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
