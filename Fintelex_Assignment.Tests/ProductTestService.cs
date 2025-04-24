using Xunit;
using Moq;
using FluentAssertions;
using Fintelex_Assignment.Entities;
using Fintelex_Assignment.Repositories.Interfaces;
using Fintelex_Assignment.Services.Implements;
using AutoMapper;
namespace Fintelex_Assignment.Tests
{
    public class ProductTestService
    {
        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnProduct_WhenExists()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Test Product",
                Description = "Test Description",
                Price = 100.0m,
                StockQuantity = 5,
                CategoryId = 2
            };

            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

            var mapperConfig = new MapperConfiguration(cfg => { });
            var mapper = mapperConfig.CreateMapper();

            var service = new ProductService(mockRepo.Object, mapper);

            // Act
            var result = await service.GetProductByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(1);
            result.Name.Should().Be("Test Product");
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnNull_WhenProductNotFound()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(999)).ReturnsAsync((Product?)null);  // Simulate not found

            var mapperConfig = new MapperConfiguration(cfg => { });
            var mapper = mapperConfig.CreateMapper();

            var service = new ProductService(mockRepo.Object, mapper);

            // Act
            var result = await service.GetProductByIdAsync(999);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnMappedProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Test Product 1",
                    Description = "Desc 1",
                    Price = 100,
                    StockQuantity = 10,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category A" }
                },
                new Product
                {
                    Id = 2,
                    Name = "Test Product 2",
                    Description = "Desc 2",
                    Price = 200,
                    StockQuantity = 20,
                    CategoryId = 2,
                    Category = new Category { Id = 2, Name = "Category B" }
                }
            };

            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

            var config = new MapperConfiguration(cfg => { });
            var mapper = config.CreateMapper();

            var service = new ProductService(mockRepo.Object, mapper);

            // Act
            var result = await service.GetAllProductsAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().Name.Should().Be("Test Product 1");
            result.First().CategoryName.Should().Be("Category A");
        }
    }
}