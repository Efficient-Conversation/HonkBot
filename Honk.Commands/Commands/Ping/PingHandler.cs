using Honk.Services;
using MediatR;

namespace Honk.Commands.Commands.Ping;

public class PingHandler : INotificationHandler<PingNotification>
{
    public async Task Handle(PingNotification notification, CancellationToken cancellationToken)
    {
        LogService.LogDebug("Entered PingHandler");
        
        await notification.Channel.SendMessageAsync("Pong!");
    }
}