using Moq;
using OneExpertInterview.Application.Interfaces;
using OneExpertInterview.Application.Services;
using OneExpertInterview.Domain.Interfaces;
using OneExpertInterview.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneExpertInterview.Tests.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<ILogger> _loggerMock;
        private readonly Mock<IOrderRepository> _repositoryMock;
        private readonly Mock<IOrderValidator> _validatorMock;
        private readonly Mock<INotificationService> _notificationMock;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _loggerMock = new Mock<ILogger>();
            _repositoryMock = new Mock<IOrderRepository>();
            _validatorMock = new Mock<IOrderValidator>();
            _notificationMock = new Mock<INotificationService>();

            _orderService = new OrderService(
                _loggerMock.Object,
                _repositoryMock.Object,
                _validatorMock.Object,
                _notificationMock.Object
            );
        }

        [Fact]
        public async Task ProcessOrderAsync_ValidOrder_ShouldProcessSuccessfully()
        {
            // Arrange
            var orderId = 1;
            var order = new { Id = orderId, ProductName = "Laptop" };

            _validatorMock.Setup(v => v.IsValid(orderId)).Returns(true);
            //_repositoryMock.Setup(r => r.GetOrder(orderId)).Returns(order);
            _repositoryMock.Setup(r => r.GetOrder(orderId)).Returns($"Laptop");


            // Act
            await _orderService.ProcessOrderAsync(orderId);

            // Assert
            _loggerMock.Verify(l => l.LogInfo(It.Is<string>(s => s.Contains("ProcessOrderAsync processed"))), Times.Once);
            _notificationMock.Verify(n => n.Send(It.Is<string>(s => s.Contains("OrderProcessed"))), Times.Once);
        }

        [Fact]
        public async Task ProcessOrderAsync_InvalidOrder_ShouldLogErrorAndReturn()
        {
            // Arrange
            var invalidOrderId = -1;
            _validatorMock.Setup(v => v.IsValid(invalidOrderId)).Returns(false);

            // Act
            await _orderService.ProcessOrderAsync(invalidOrderId);

            // Assert
            _loggerMock.Verify(l => l.LogError(It.Is<string>(s => s.Contains("validation failed")),
                                               It.IsAny<ArgumentException>()), Times.Once);
            _repositoryMock.Verify(r => r.GetOrder(It.IsAny<int>()), Times.Never);
            _notificationMock.Verify(n => n.Send(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task ProcessOrderAsync_OrderNotFound_ShouldCatchExceptionAndLogError()
        {
            // Arrange
            var orderId = 999888777;

            _validatorMock.Setup(v => v.IsValid(orderId)).Returns(true);
            _repositoryMock.Setup(r => r.GetOrder(orderId)).Throws(new InvalidOperationException("Order not found"));

            // Act
            await _orderService.ProcessOrderAsync(orderId);

            // Assert
            _loggerMock.Verify(l => l.LogError(It.Is<string>(s => s.Contains("Failed processing")),
                                               It.IsAny<InvalidOperationException>()), Times.Once);
            _notificationMock.Verify(n => n.Send(It.IsAny<string>()), Times.Never);
        }
    }
}
