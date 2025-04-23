using Fintelex_Assignment.Entities;

namespace Fintelex_Assignment.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Entities.Product>
    {
        Task<IEnumerable<Entities.Product>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<Entities.Product>> SearchProductsAsync(string searchTerm);
        Task<IEnumerable<Entities.Product>> GetPagedProductsAsync(int page, int pageSize, string sortBy, bool ascending);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}
