using OneExpertInterview.Application.Interfaces;
using OneExpertInterview.Application.Services;
using OneExpertInterview.Application.Validators;
using OneExpertInterview.Domain.Entities;
using OneExpertInterview.Domain.Interfaces;
using OneExpertInterview.Infrastructure.Logging;
using OneExpertInterview.Infrastructure.Repositories;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace OneExpertInterview
{
    internal class Program
    {
        static SimpleContainer _simpleContainer = new SimpleContainer();

        static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            var config = builder.Configuration;
            _simpleContainer.RegisterInstance<IConfiguration>(config);

            // Everything is registered as Singleton for simplicity, not beacause I don't know other lifetimes :) Scoped or Transient
            _simpleContainer.RegisterSingleton<IOrderRepository, OrderRepository>();
            _simpleContainer.RegisterSingleton<ILogger, ConsoleLogger>();
            _simpleContainer.RegisterSingleton<IOrderService, OrderService>();
            _simpleContainer.RegisterSingleton<IOrderValidator, OrderValidator>();
            _simpleContainer.RegisterSingleton<INotificationService, NotificationService>();

            InitOrderRepository();

            var orderService = _simpleContainer.GetService<IOrderService>();

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

        private static void InitOrderRepository()
        {
            var orderRepository = _simpleContainer.GetService<IOrderRepository>();
            orderRepository.AddOrder(new Order() { Id = 1, Description = "Laptop" });
            orderRepository.AddOrder(new Order() { Id = 2, Description = "Phone" });
        }
    }
}
