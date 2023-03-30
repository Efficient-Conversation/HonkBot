using System;
using Honk.Services.Models.Enums;

namespace Honk.Services;

public static class ConfigurationService
{
    public static string? GetConfigValue(ConfigKey key)
    {
        string keyToCheck = key.ToString();
        string? valueFromEnv = Environment.GetEnvironmentVariable(keyToCheck);
        return valueFromEnv;
    }
}