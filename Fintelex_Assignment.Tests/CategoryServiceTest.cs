using Xunit;
using Moq;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Fintelex_Assignment.Entities;
using Fintelex_Assignment.Repositories.Interfaces;
using Fintelex_Assignment.Services.Implements;
using Fintelex_Assignment.Dtos;

namespace Fintelex_Assignment.Tests
{
    public class CategoryServiceTest
    {
        [Fact]
        public async Task GetAllCategoriesAsync_ShouldReturnAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Fruits", Description = "Fresh fruits" },
                new Category { Id = 2, Name = "Vegetables", Description = "Green and fresh" }
            };

            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(categories);

            var mockLogger = new Mock<ILogger<CategoryService>>();

            var service = new CategoryService(mockRepo.Object)
            {
                logger = mockLogger.Object  // Manually injecting logger
            };

            // Act
            var result = await service.GetAllCategoriesAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(c => c.Name == "Fruits");
            result.Should().Contain(c => c.Name == "Vegetables");
        }

        [Fact]
        public async Task DeleteCategoryAsync_ShouldReturnTrue_WhenCategoryExists()
        {
            // Arrange
            var category = new Category { Id = 2, Name = "ToDelete" };

            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(category);
            mockRepo.Setup(r => r.DeleteAsync(category)).Returns(Task.CompletedTask);

            var service = new CategoryService(mockRepo.Object);

            // Act
            var result = await service.DeleteCategoryAsync(2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateCategoryAsync_ShouldUpdateAndReturnTrue_WhenCategoryExists()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Old", Description = "Old desc" };
            var dto = new CategoryDto { Id = 1, Name = "Updated", Description = "New desc" };

            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(category);
            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Category>())).Returns(Task.CompletedTask);

            var service = new CategoryService(mockRepo.Object);

            // Act
            var result = await service.UpdateCategoryAsync(dto);

            // Assert
            result.Should().BeTrue();
            category.Name.Should().Be("Updated");
            category.Description.Should().Be("New desc");
        }

        [Fact]
        public async Task CreateCategoryAsync_ShouldReturnCreatedCategory()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Snacks", Description = "Tasty snacks" };

            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(r => r.AddAsync(category)).ReturnsAsync(category);

            var service = new CategoryService(mockRepo.Object);

            // Act
            var result = await service.CreateCategoryAsync(category);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Snacks");
        }



    }
}
