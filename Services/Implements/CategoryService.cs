using Fintelex_Assignment.Dtos;
using Fintelex_Assignment.Entities;
using Fintelex_Assignment.Repositories.Interfaces;
using Fintelex_Assignment.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Fintelex_Assignment.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryService> _logger;
        private ILogger<CategoryService>? logger;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {

            _logger.LogDebug("Starting GetAllCategoriesAsync...");

            var categories = await _categoryRepository.GetAllAsync();

            _logger.LogDebug("Retrieved {Count} categories from repository.", categories.Count());

            return categories;
        }

       

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            return await _categoryRepository.AddAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _categoryRepository.UpdateAsync(category);
        }

       

        public async Task<IEnumerable<Category>> GetCategoriesWithProductsAsync()
        {
            return await _categoryRepository.GetCategoriesWithProductsAsync();
        }

        public async Task<bool> UpdateCategoryAsync(CategoryDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.Id);
            if (category == null) return false;

            category.Name = dto.Name;
            category.Description = dto.Description;

            await _categoryRepository.UpdateAsync(category);
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return false;

            await _categoryRepository.DeleteAsync(category);
            return true;
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

    }

}
