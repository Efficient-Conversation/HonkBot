using Discord.WebSocket;
using Honk.Commands.Commands.MagicEightBall;
using Honk.Commands.Commands.Ping;
using MediatR;

namespace Honk.Commands;

public static class CommandParser
{
    public const char CommandPrefix = ';';

    public static bool HasCommandPrefix(string messageText)
    {
        return messageText.StartsWith(CommandPrefix);
    }

    public static async Task<bool> TrySendCommand(IMediator mediator, SocketMessage message)
    {
        List<string> commandParts = message.Content.Split(' ').ToList();
        string command = commandParts[0].Remove(0, 1);

        switch (command.ToLowerInvariant())
        {
            case "ping":
                await mediator.Publish(new PingNotification(message.Channel));
                return true;
            case "8ball":
                await mediator.Publish(new MagicEightBallNotification(message.Author, message.Channel));
                return true;
        }

        return false;
    }
}