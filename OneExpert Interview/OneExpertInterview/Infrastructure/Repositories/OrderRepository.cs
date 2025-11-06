using OneExpertInterview.Application.Interfaces;
using OneExpertInterview.Domain.Entities;
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
        }

        public string GetOrder(int orderId)
        {
            if (orderId <= 0)
                throw new ArgumentException("orderId must be greater than 0");

            if (!_orders.TryGetValue(orderId, out var product))
                throw new KeyNotFoundException($"Order with id {orderId} was not found");

            return product;
        }

        public void AddOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            if (order.Id <= 0)
                throw new ArgumentException("Order ID must be greater than 0", nameof(order.Id));
            if (string.IsNullOrWhiteSpace(order.Description))
                throw new ArgumentException("Order description cannot be empty", nameof(order.Description));

            if (!_orders.TryAdd(order.Id, order.Description))
                throw new InvalidOperationException($"An order with ID {order.Id} already exists");
        }
    }
}
