using Discord.WebSocket;
using Honk.Responses.Tristan.ShutUpNerd;
using Honk.Services;
using Honk.Services.Models.Enums;
using MediatR;

namespace Honk.Responses;

public static class ResponseParser
{
    private static readonly Random _random = new();
    public static async Task RespondIfNeeded(IMediator mediator, SocketMessage message)
    {
        People? author = EffyConvoService.GetPersonById(message.Author.Id);
        switch (author)
        {
            case People.Alex:
                break;
            case People.Copy:
                break;
            case People.Filly:
                break;
            case People.Monkey:
                break;
            case People.Name:
                break;
            case People.Pedro:
                break;
            case People.Saeryn:
                break;
            case People.Smigel:
                break;
            case People.Tristan:
                if (_random.Next(1000) == 1)
                {
                    await mediator.Publish(new ShutUpNerdNotification(message));
                }
                break;
            case People.Trossman:
                break;
            case People.V:
                break;
            default:
                break;
        }
    }
}