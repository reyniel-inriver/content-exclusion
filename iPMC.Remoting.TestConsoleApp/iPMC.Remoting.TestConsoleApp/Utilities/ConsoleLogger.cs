using System;
using inRiver.Remoting.Extension;
using inRiver.Remoting.Log;

namespace iPMC.Remoting.TestConsoleApp.Utilities;

public class ConsoleLogger : IExtensionLog
{
    public void Log(LogLevel level, string message)
    {
        Console.WriteLine($"{DateTime.Now:s}:{level}: {message}");
    }

    public void Log(LogLevel level, string message, Exception ex)
    {
        Console.WriteLine($"{DateTime.Now:s}:{level}: {message}");
        Console.WriteLine($"Error Message: {ex.Message}");
        Console.WriteLine($"StackTrace: {ex.StackTrace}");
    }
}