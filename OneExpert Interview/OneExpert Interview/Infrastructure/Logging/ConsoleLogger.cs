using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneExpertInterview.Infrastructure.Logging
{
    public enum LogLevel
    {
        Info,
        Error
    }

    public interface ILogger
    {
        void LogInfo(string message);
        void LogError(string message, Exception ex);
    }

    public class ConsoleLogger : ILogger
    {
        private LogLevel _logLevel;

        private string Timestamp() => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        public ConsoleLogger(IConfiguration config)
        {
            var logLevelStr = config?["LogLevel"] ?? "";

            if (!Enum.TryParse(logLevelStr, ignoreCase: true, out _logLevel))
            {
                LogError($"Invalid LogLevel configuration: '{logLevelStr}' -> default 'Info' level", new ArgumentOutOfRangeException());
            }
        }

        public void LogInfo(string message)
        {
            if (_logLevel > (int)LogLevel.Info)
                return;

            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"INFO {Timestamp()}: {message}");
            Console.ForegroundColor = currentColor;
        }

        public void LogError(string message, Exception ex)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR {Timestamp()}: {message}, ex: {ex.Message}");
            Console.ForegroundColor = currentColor;
        }
    }
}
