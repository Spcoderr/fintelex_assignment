using Fintelex_Assignment.Dtos;
using Fintelex_Assignment.Entities;

namespace Fintelex_Assignment.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
       
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
        Task<IEnumerable<Product>> GetPagedProductsAsync(int page, int pageSize, string sortBy, bool ascending);
        Task<bool> UpdateProductAsync(ProductDto dto);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    }
}
