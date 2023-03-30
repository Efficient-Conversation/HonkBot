using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Honk.Services.Extensions;

public class EnumExtensions
{
    public static T GetDefaultValue<T>() where T : struct, Enum
    {
        T? defaultValue = Enum.GetValues(typeof(T))
            .OfType<T>()
            .FirstOrDefault();

        if (defaultValue == null)
        {
            throw new InvalidCastException();
        }

        return defaultValue.Value;
    }
    
    public static T GetRandomValue<T>() where T : struct, Enum
    {
        T? randomValue = Enum.GetValues(typeof(T))
            .OfType<T>()
            .OrderBy(_ => Guid.NewGuid())
            .FirstOrDefault();

        if (randomValue == null)
        {
            throw new InvalidCastException();
        }

        return randomValue.Value;
    }
}