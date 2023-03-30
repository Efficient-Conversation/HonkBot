using Discord.WebSocket;
using Honk.Events.MessageReceived;
using Honk.Events.Ready;
using Honk.Services;
using MediatR;

namespace Honk;

public class DiscordEventListener
{
    private readonly DiscordSocketClient _client;
    private readonly IMediator _mediator;

    public DiscordEventListener(DiscordSocketClient client, IMediator mediator)
    {
        _client = client;
        _mediator = mediator;
    }
    
    public DiscordSocketClient StartListeners()
    {
        _client.Log += LogService.LogDiscordDotnetMessage;
        _client.Ready += OnReadyAsync;
        _client.MessageReceived += OnMessageReceivedAsync;

        return _client;
    }

    private Task OnMessageReceivedAsync(SocketMessage arg)
    {
        return _mediator.Publish(new MessageReceivedNotification(arg));
    }
    
    private Task OnReadyAsync()
    {
        return _mediator.Publish(ReadyNotification.Default);
    }
}