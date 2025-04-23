using Fintelex_Assignment.Dtos;
using Fintelex_Assignment.Entities;

namespace Fintelex_Assignment.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        
        Task<Category> CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
       
        Task<IEnumerable<Category>> GetCategoriesWithProductsAsync();
        Task<bool> UpdateCategoryAsync(CategoryDto dto);
        Task<bool> DeleteCategoryAsync(int id);
        Task<CategoryDto?> GetCategoryByIdAsync(int id);
    }
}
