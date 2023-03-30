using Discord.WebSocket;
using MediatR;

namespace Honk.Commands.Commands.Ping;

public class PingNotification : INotification
{
    public ISocketMessageChannel Channel { get; }
    
    public PingNotification(ISocketMessageChannel channel)
    {
        Channel = channel;
    }
}