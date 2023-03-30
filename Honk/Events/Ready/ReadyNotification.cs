using MediatR;

namespace Honk.Events.Ready;

public class ReadyNotification : INotification
{
    public static readonly ReadyNotification Default = new();

    private ReadyNotification()
    {
    }
}