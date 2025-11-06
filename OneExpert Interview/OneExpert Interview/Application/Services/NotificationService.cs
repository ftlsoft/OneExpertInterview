using OneExpertInterview.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneExpertInterview.Application.Services
{
    public class NotificationService : INotificationService
    {
        public void Send(string message)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"NewMessage: {message}");
            Console.ForegroundColor = color;
        }
    }
}
