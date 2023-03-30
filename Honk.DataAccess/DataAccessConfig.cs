using System;
using System.IO;
using Honk.Services;

namespace Honk.DataAccess;

/// <summary>
///   For now this is just using Json files, plan on updating to use either MongoDB or Postgres in the future.
/// </summary>
public static class DataAccessConfig
{
    static DataAccessConfig()
    {
        if (!Directory.Exists(_defaultDataDir))
        {
            LogService.LogInfo($"Creating data directory: {_defaultDataDir}");
            Directory.CreateDirectory(_defaultDataDir);
        }
    }
    
    private static readonly string _defaultDataDir =
        $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/HonkBot";
    
    public static readonly string AdminUsers = $"{_defaultDataDir}/AdminUsers.json";
    public static readonly string BannedUsers = $"{_defaultDataDir}/BannedUsers.json";
}