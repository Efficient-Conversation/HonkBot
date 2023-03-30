using System;
using System.Collections.Generic;
using System.Linq;
using Honk.Services.Models.Enums;

namespace Honk.Services;

public record Member(People Person, ulong Id, List<Credentials> Access);

public static class EffyConvoService
{
    
    public static readonly List<Member> People = new()
    {
        new (Models.Enums.People.Alex, 217746443459362826UL, new()),
        new (Models.Enums.People.Copy, 129145536950435840UL, new() { Credentials.BotAdmin }),
        new (Models.Enums.People.Filly, 132194909699440640UL, new()),
        new (Models.Enums.People.Pedro, 135900009207037952UL, new()),
        new (Models.Enums.People.Monkey, 132172962504638464UL, new() { Credentials.BotAdmin, Credentials.BotHost }),
        new (Models.Enums.People.Name, 129145945735561216UL, new()),
        new (Models.Enums.People.Saeryn, 129642843617624064UL, new()),
        new (Models.Enums.People.Smigel, 132183968769507328UL, new()),
        new (Models.Enums.People.Trossman, 181476008598175744UL, new()),
        new (Models.Enums.People.V, 272552418007318528UL, new())
    };
    
    public static People GetRandomPerson()
    {
        return People
            .OrderBy(_ => Guid.NewGuid())
            .First().Person;
    }
    
    public static People? GetPersonById(ulong id)
    {
        try
        {
            return People.First(each => each.Id == id).Person;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
        
    }
    
    public static ulong GetIdByPerson(People person)
    {
        return People.First(each => each.Person == person).Id;
    }
}

