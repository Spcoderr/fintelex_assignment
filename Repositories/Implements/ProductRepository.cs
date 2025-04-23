using Fintelex_Assignment.Dbcontext;
using Fintelex_Assignment.Entities;
using Fintelex_Assignment.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fintelex_Assignment.Repositories.Implements
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        /* public override async Task<IEnumerable<Product>> GetAllAsync()
         {
             return await _context.Products.Include(p => p.Category).ToListAsync();
         }

         public override async Task<Product> GetByIdAsync(int id)
         {
             return await _context.Products
                 .Include(p => p.Category)
                 .FirstOrDefaultAsync(p => p.Id == id);
         }*/

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }


        public async Task<IEnumerable<Product>> GetPagedProductsAsync(int page, int pageSize, string sortBy, bool ascending)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            // Sort using (name/price) property
            switch (sortBy.ToLower())
            {
                case "name":
                    query = ascending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
                    break;
                case "price":
                    query = ascending ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
                    break;
                default:
                    query = ascending ? query.OrderBy(p => p.Id) : query.OrderByDescending(p => p.Id);
                    break;
            }

            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Where(p => !p.IsDeleted)
                .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            product.IsDeleted = true;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }

}
