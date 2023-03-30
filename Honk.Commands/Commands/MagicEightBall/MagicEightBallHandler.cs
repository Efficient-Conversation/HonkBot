using Honk.Services;
using Honk.Services.Models.Enums;
using MediatR;

namespace Honk.Commands.Commands.MagicEightBall;

public class MagicEightBallHandler : INotificationHandler<MagicEightBallNotification>
{
    private const string PERSON_PLACEHOLDER = "{{person}}";

    private static readonly Random _random = new Random();
    
    /// <summary>
    ///   Most of these are generic 8ball quotes, but some are from our own stuff<br />
    ///    answers[0][x] = Positive<br />
    ///    answers[1][x] = Unsure<br />
    ///    answers[2][x] = Negative<br />
    /// </summary>
    private static readonly List<List<string>> _answers = new() {
        // Positive
        new() {
            "It is certain", 
            "It is decidedly so", 
            "Without a doubt", 
            "Yes definitely",
            "You may rely on it", 
            "As I see it, yes", 
            "Most likely", 
            "Outlook good", 
            "Yes",
            "Signs point to yes", 
            "Have a secure day"
        },
        // Unsure
        new() {
            "Reply hazy try again", 
            "Ask again later", 
            "Better not tell you now",
            "Cannot predict now", 
            "Concentrate and ask again", 
            "I am the law",
            "God made tomorrow for the crooks we don't catch today", 
            "You can't out run a radio",
            $"I don't know, ask {PERSON_PLACEHOLDER}"
        },
        // Negative
        new() {
            "Don't count on it", 
            "My reply is no", 
            "My sources say no", 
            "Outlook not so good",
            "Very doubtful", 
            "Criminal detected", 
            "Prepare for justice", 
            "Freeze scumbag"
        }
    };
    
    public async Task Handle(MagicEightBallNotification notification, CancellationToken cancellationToken)
    {
        LogService.LogDebug("Entered MagicEightBallHandler");

        People randomPerson;
        do
        {
            randomPerson = EffyConvoService.GetRandomPerson();
        } while (notification.Author != null && randomPerson == notification.Author);

        List<string> certainty = _answers[_random.Next(300) % 3];
        string response = certainty[_random.Next(certainty.Count)]
            .Replace(PERSON_PLACEHOLDER, Enum.GetName(randomPerson));
        
        await notification.Channel.SendMessageAsync(response);
    }
}