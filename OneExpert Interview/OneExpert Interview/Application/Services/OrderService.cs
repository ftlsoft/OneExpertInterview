using OneExpertInterview.Application.Interfaces;
using OneExpertInterview.Domain.Interfaces;
using OneExpertInterview.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneExpertInterview.Application.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly ILogger _logger;

        public OrderService(IOrderRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task ProcessOrderAsync(int orderId)
        {
            _logger.LogInfo($"ProcessOrderAsync orderId: {orderId}");

            try
            {
                _logger.LogInfo($"ProcessOrderAsync get orderId: {orderId}");
                var product = _repository.GetOrder(orderId);

                await Task.Delay(100);

                _logger.LogInfo($"ProcessOrderAsync processed orderId: {orderId}: {product}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"ProcessOrderAsync Failed processing orderId: {orderId}", ex);
            }
        }
    }
}
