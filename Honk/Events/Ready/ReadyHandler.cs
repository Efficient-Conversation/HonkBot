using System.Reflection;
using Discord;
using Discord.WebSocket;
using Honk.Services;
using Honk.Services.Models.Enums;
using MediatR;

namespace Honk.Events.Ready;

public class ReadyHandler : INotificationHandler<ReadyNotification>
{
    private readonly DiscordSocketClient _client;
    
    public ReadyHandler(DiscordSocketClient client)
    {
        _client = client;
    }
    
    public async Task Handle(ReadyNotification notification, CancellationToken cancellationToken)
    {
        LogService.LogDebug("Entered ReadyHandler");

        Version? version = typeof(HonkBot).Assembly.GetName().Version;
        
        foreach (Member person in EffyConvoService.People.Where(person => person.Access.Contains(Credentials.BotHost)))
        {
            await _client.GetUser(person.Id).SendMessageAsync($"H.O.N.K. Bot startup complete. v{version}");
        }
    }
}
