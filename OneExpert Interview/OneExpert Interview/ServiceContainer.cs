using OneExpertInterview.Application.Interfaces;
using OneExpertInterview.Application.Services;
using OneExpertInterview.Domain.Interfaces;
using OneExpertInterview.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneExpertInterview
{
    public static class ServiceContainer
    {
        public static ILogger GetLogger()
        {
            return new ConsoleLogger();
        }

        public static IOrderRepository GetOrderRepository()
        {
            return new Infrastructure.Repositories.OrderRepository();
        }

        public static IOrderService GetOrderService()
        {
            return new OrderService(
                repository: GetOrderRepository(),
                logger: GetLogger()
            );
        }
    }
}
