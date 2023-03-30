using Discord;
using Honk.Services;
using MediatR;

namespace Honk.Responses.Tristan.ShutUpNerd;

public class ShutUpNerdHandler : INotificationHandler<ShutUpNerdNotification>
{
    public async Task Handle(ShutUpNerdNotification notification, CancellationToken cancellationToken)
    {
        LogService.LogDebug("Entered ShutUpNerdHandler");

        await notification.Channel.SendMessageAsync("shut up nerd", messageReference: notification.ReplyTo);
    }
}