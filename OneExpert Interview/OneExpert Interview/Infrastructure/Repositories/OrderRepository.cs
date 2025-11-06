using OneExpertInterview.Application.Interfaces;
using OneExpertInterview.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneExpertInterview.Infrastructure.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly ConcurrentDictionary<int, string> _orders = new ConcurrentDictionary<int, string>();

        public OrderRepository()
        {
            if (!_orders.TryAdd(1, "Laptop"))
                throw new Exception("Failed to add initial order.");
            if (!_orders.TryAdd(2, "Phone"))
                throw new Exception("Failed to add initial order.");
        }

        public string GetOrder(int orderId)
        {
            if (orderId <= 0)
                throw new ArgumentException("orderId must be greater than 0");

            if (!_orders.TryGetValue(orderId, out var product))
                throw new KeyNotFoundException($"Order with id {orderId} was not found.");

            return product;
        }
    }
}
