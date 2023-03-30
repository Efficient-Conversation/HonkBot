using System;
using System.Threading.Tasks;
using Discord;
using Serilog;
using Serilog.Events;

namespace Honk.Services;

public static class LogService
{
    static LogService()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();
    }
    
    public static Task LogDiscordDotnetMessage(LogMessage message)
    {
        LogEventLevel severity = message.Severity switch
        {
            LogSeverity.Critical => LogEventLevel.Fatal,
            LogSeverity.Error => LogEventLevel.Error,
            LogSeverity.Warning => LogEventLevel.Warning,
            LogSeverity.Info => LogEventLevel.Information,
            LogSeverity.Verbose => LogEventLevel.Verbose,
            LogSeverity.Debug => LogEventLevel.Debug,
            _ => LogEventLevel.Information
        };

        Log.Write(severity, message.Exception, "[{Source}] {Message}", message.Source, message.Message);

        return Task.CompletedTask;
    }

    public static void LogError(string message, Exception? exception = null)
    {
        Log.Write(LogEventLevel.Error, exception, "{Message}", message);
    }

    public static void LogWarning(string message)
    {
        Log.Write(LogEventLevel.Warning, (Exception?)null, "{Message}", message);
    }

    public static void LogInfo(string message)
    {
        Log.Write(LogEventLevel.Information, (Exception?)null, "{Message}", message);
    }

    public static void LogDebug(string message)
    {
        Log.Write(LogEventLevel.Debug, (Exception?)null, "{Message}", message);
    }
}