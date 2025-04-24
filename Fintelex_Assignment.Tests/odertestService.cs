using Xunit;
using Moq;
using FluentAssertions;
using Fintelex_Assignment.Entities;
using Fintelex_Assignment.Repositories.Interfaces;
using Fintelex_Assignment.Services.Implements;
using System;
using System.Threading.Tasks;

namespace Fintelex_Assignment.Tests
{
    public class OrderServiceTest
    {
        [Fact]
        public async Task GetOrderByIdAsync_ShouldReturnOrder_WhenExists()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                OrderDate = DateTime.UtcNow,
                CustomerId = 5,
                TotalAmount = 1500.75m,
                Status = "Pending"
            };

            var mockRepo = new Mock<IOrderRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(order);

            var service = new OrderService(mockRepo.Object);

            // Act
            var result = await service.GetOrderByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Status.Should().Be("Pending");
            result.TotalAmount.Should().Be(1500.75m);
        }

        [Fact]
        public async Task GetOrdersByCustomerAsync_ShouldReturnOrders_ForCustomer()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = 1, CustomerId = 10, TotalAmount = 150 },
                new Order { Id = 2, CustomerId = 10, TotalAmount = 300 }
            };

            var mockRepo = new Mock<IOrderRepository>();
            mockRepo.Setup(r => r.GetOrdersByCustomerAsync(10)).ReturnsAsync(orders);

            var service = new OrderService(mockRepo.Object);

            // Act
            var result = await service.GetOrdersByCustomerAsync(10);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().OnlyContain(o => o.CustomerId == 10);
        }

        [Fact]
        public async Task GetOrdersByStatusAsync_ShouldReturnOrders_WithMatchingStatus()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = 1, Status = "Pending", TotalAmount = 100 },
                new Order { Id = 2, Status = "Pending", TotalAmount = 200 }
            };

            var mockRepo = new Mock<IOrderRepository>();
            mockRepo.Setup(r => r.GetOrdersByStatusAsync("Pending")).ReturnsAsync(orders);

            var service = new OrderService(mockRepo.Object);

            // Act
            var result = await service.GetOrdersByStatusAsync("Pending");

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().OnlyContain(o => o.Status == "Pending");
        }
    }
}
