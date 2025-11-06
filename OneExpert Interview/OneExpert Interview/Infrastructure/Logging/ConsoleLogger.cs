using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneExpertInterview.Infrastructure.Logging
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogError(string message, Exception ex);
    }

    public class ConsoleLogger : ILogger
    {
        private string Timestamp() => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        public void LogInfo(string message)
        {
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
