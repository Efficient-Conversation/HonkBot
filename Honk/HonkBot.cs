using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Honk.Commands;
using Honk.Responses;
using Honk.Services;
using Honk.Services.Models.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Honk;

public static class HonkBot
{
    public static async Task Main()
    {
        IHost host = Host.CreateDefaultBuilder()
            .ConfigureServices(Setup)
            .Build(); 

        IServiceProvider services = host.Services;

        DiscordEventListener listener = services.GetRequiredService<DiscordEventListener>();
        DiscordSocketClient client = listener.StartListeners();
        
        await client.LoginAsync(TokenType.Bot, GetHonkBotToken());
        await client.StartAsync();
        await Task.Delay(Timeout.Infinite);
    }
    
    private static void Setup(HostBuilderContext hostContext, IServiceCollection services)
    {
        services.AddMediatR(cfg=>
        {
            cfg.RegisterServicesFromAssemblies(
                typeof(HonkBot).Assembly,
                typeof(CommandParser).Assembly,
                typeof(ResponseParser).Assembly
            );
        });
        
        services.AddSingleton(new DiscordSocketClient(new()
        {
            AlwaysDownloadUsers = true,
            MessageCacheSize = 100,
            GatewayIntents = GatewayIntents.All,
            LogLevel = LogSeverity.Info
        }));
        
        services.AddSingleton<DiscordEventListener>();
        services.AddSingleton(serviceProvider => new InteractionService(serviceProvider.GetRequiredService<DiscordSocketClient>()));
    }

    private static string GetHonkBotToken()
    {
        string? token = ConfigurationService.GetConfigValue(ConfigKey.HONKBOT_TOKEN);

        if (string.IsNullOrWhiteSpace(token))
        {
            LogService.LogError($"Invalid token in environment, please check your {ConfigKey.HONKBOT_TOKEN} value."
                                + $"\nValue is currently: {token ?? "Not set"}");
            Environment.Exit(1);
        }

        return token;
    }
}