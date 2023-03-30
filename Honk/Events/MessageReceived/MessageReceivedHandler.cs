using Honk.Commands;
using Honk.Responses;
using Honk.Services;
using MediatR;

namespace Honk.Events.MessageReceived;

public class MessageReceivedHandler : INotificationHandler<MessageReceivedNotification>
{
    private readonly IMediator _mediator;
    public MessageReceivedHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task Handle(MessageReceivedNotification notification, CancellationToken cancellationToken)
    {
        LogService.LogDebug("Entered MessageReceivedHandler");
        
        if (CommandParser.HasCommandPrefix(notification.Message.Content))
        {
            bool successful = await CommandParser.TrySendCommand(_mediator, notification.Message);

            if (!successful)
            {
                LogService.LogInfo($"Invalid command received: {notification.Message.Content}");
            }
        }
        else
        {
            await ResponseParser.RespondIfNeeded(_mediator, notification.Message);
        }
    }
}