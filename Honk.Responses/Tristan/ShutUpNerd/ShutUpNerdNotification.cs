using Discord;
using Discord.WebSocket;
using Honk.Services;
using Honk.Services.Models.Enums;
using MediatR;

namespace Honk.Responses.Tristan.ShutUpNerd;

public class ShutUpNerdNotification : INotification
{
    public MessageReference ReplyTo { get; }
    public ISocketMessageChannel Channel { get; }

    public ShutUpNerdNotification(SocketMessage message)
    {
        ReplyTo = message.Reference;
        Channel = message.Channel;
    }
}