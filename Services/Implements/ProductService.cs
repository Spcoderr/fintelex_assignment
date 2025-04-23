using AutoMapper;
using Fintelex_Assignment.Dtos;
using Fintelex_Assignment.Entities;
using Fintelex_Assignment.Repositories.Interfaces;
using Fintelex_Assignment.Services.Interfaces;

namespace Fintelex_Assignment.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,  IMapper mapper)
        {
            _productRepository = productRepository;
            
            _mapper = mapper;

        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            return await _productRepository.AddAsync(product);
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }


        public async Task<bool> UpdateProductAsync(ProductDto dto)
        {
            var product = await _productRepository.GetByIdAsync(dto.Id);
            if (product == null) return false;

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;
            product.CategoryId = dto.CategoryId;

            await _productRepository.UpdateAsync(product);
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return false;

            await _productRepository.DeleteAsync(product);
            return true;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name
            }).ToList();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _productRepository.GetProductsByCategoryAsync(categoryId);
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
        {
            return await _productRepository.SearchProductsAsync(searchTerm);
        }

        public async Task<IEnumerable<Product>> GetPagedProductsAsync(int page, int pageSize, string sortBy, bool ascending)
        {
            return await _productRepository.GetPagedProductsAsync(page, pageSize, sortBy, ascending);
        }
    }
}
