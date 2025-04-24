using Xunit;
using Moq;
using FluentAssertions;
using Fintelex_Assignment.Entities;
using Fintelex_Assignment.Repositories.Interfaces;
using Fintelex_Assignment.Services.Implements;
using AutoMapper;
using Fintelex_Assignment.Dtos;

namespace Fintelex_Assignment.Tests
{
    public class CustomerServiceTest
    {
        [Fact]
        public async Task GetCustomerByIdAsync_ShouldReturnCustomer_WhenExists()
        {
            // Arrange
            var customer = new Customer
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                Address = "123 Main St"
            };

            var mockRepo = new Mock<ICustomerRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(customer);

            var config = new MapperConfiguration(cfg => { });
            var mapper = config.CreateMapper();

            var service = new CustomerService(mockRepo.Object, mapper);

            // Act
            var result = await service.GetCustomerByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.FirstName.Should().Be("John");
            result.Email.Should().Be("john.doe@example.com");
        }

        [Fact]
        public async Task GetCustomerWithOrdersAsync_ShouldReturnMappedCustomerDto()
        {
            // Arrange
            var customer = new Customer
            {
                Id = 1,
                FirstName = "Alice",
                LastName = "Smith",
                Email = "alice@example.com",
                Phone = "123456789",
                Address = "123 Apple St",
                Orders = new List<Order>
                {
                    new Order { Id = 101, CustomerId = 1 }
                }
            };

            var customerDto = new CustomerDto
            {
                Id = 1,
                FirstName = "Alice",
                LastName = "Smith",
                Email = "alice@example.com",
                Phone = "123456789",
                Address = "123 Apple St"
                // You can add orders if needed
            };

            var mockRepo = new Mock<ICustomerRepository>();
            mockRepo.Setup(repo => repo.GetCustomerWithOrdersAsync(1)).ReturnsAsync(customer);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, CustomerDto>();
            });
            var mapper = mapperConfig.CreateMapper();

            var service = new CustomerService(mockRepo.Object, mapper);

            // Act
            var result = await service.GetCustomerWithOrdersAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.FirstName.Should().Be("Alice");
            result.Email.Should().Be("alice@example.com");
        }

        [Fact]
        public async Task GetAllCustomersAsync_ShouldReturnMappedCustomerDtos()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    FirstName = "Alice",
                    LastName = "Smith",
                    Email = "alice@example.com",
                    Phone = "123456789",
                    Address = "123 Apple St"
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "Bob",
                    LastName = "Johnson",
                    Email = "bob@example.com",
                    Phone = "987654321",
                    Address = "456 Orange Ave"
                }
            };

            var mockRepo = new Mock<ICustomerRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(customers);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, CustomerDto>();
            });
            var mapper = mapperConfig.CreateMapper();

            var service = new CustomerService(mockRepo.Object, mapper);

            // Act
            var result = await service.GetAllCustomersAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(dto => dto.Email == "alice@example.com");
            result.Should().Contain(dto => dto.FirstName == "Bob");
        }
    }
}
