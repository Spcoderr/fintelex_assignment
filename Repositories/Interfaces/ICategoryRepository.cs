using Fintelex_Assignment.Entities;

namespace Fintelex_Assignment.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Entities.Category>
    {
        Task<IEnumerable<Entities.Category>> GetCategoriesWithProductsAsync();
        Task<Category?> GetByIdAsync(int id);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}
