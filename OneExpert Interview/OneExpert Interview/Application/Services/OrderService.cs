using OneExpertInterview.Application.Interfaces;
using OneExpertInterview.Domain.Interfaces;
using OneExpertInterview.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneExpertInterview.Application.Services
{
    internal class OrderService : IOrderService
    {
        private readonly ILogger _logger;
        private readonly IOrderRepository _repository;
        private readonly IOrderValidator _orderValidator;

        public OrderService(ILogger logger, IOrderRepository repository, IOrderValidator orderValidator)
        {
            _logger = logger;
            _repository = repository;
            _orderValidator = orderValidator;
        }

        public async Task ProcessOrderAsync(int orderId)
        {
            _logger.LogInfo($"ProcessOrderAsync orderId: {orderId}");

            if (!_orderValidator.IsValid(orderId))
            {
                _logger.LogError($"Order validation failed for orderId: {orderId}", new ArgumentException("Invalid order ID."));
                return;
            }

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
