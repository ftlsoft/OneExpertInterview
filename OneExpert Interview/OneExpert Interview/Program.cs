using OneExpertInterview.Application.Interfaces;
using OneExpertInterview.Application.Services;
using OneExpertInterview.Domain.Interfaces;
using OneExpertInterview.Infrastructure.Logging;
using OneExpertInterview.Infrastructure.Repositories;
using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace OneExpertInterview
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            //var orderService = ServiceContainer.GetOrderService();
            using var simpleContainer = new SimpleContainer();
            simpleContainer.RegisterSingleton<IOrderRepository, OrderRepository>();
            simpleContainer.RegisterSingleton<ILogger, ConsoleLogger>();
            simpleContainer.RegisterSingleton<IOrderService, OrderService>();

            var orderService = simpleContainer.GetService<IOrderService>();

            Console.WriteLine("Order Processing System");

            var tasks = new List<Task>
            {
                orderService.ProcessOrderAsync(1),
                orderService.ProcessOrderAsync(2),
                orderService.ProcessOrderAsync(-1)
            };

            await Task.WhenAll(tasks);

            Console.WriteLine("All orders processed.");
        }
    }
}
