using Discord.WebSocket;
using Honk.Services;
using Honk.Services.Models.Enums;
using MediatR;

namespace Honk.Commands.Commands.MagicEightBall;

public class MagicEightBallNotification : INotification
{
    public People? Author { get; }
    public ISocketMessageChannel Channel { get; }
    
    public MagicEightBallNotification(SocketUser author, ISocketMessageChannel channel)
    {
        Author = EffyConvoService.GetPersonById(author.Id);
        Channel = channel ?? throw new ArgumentNullException(nameof(channel));
    }
}