using Fintelex_Assignment.Dbcontext;
using Fintelex_Assignment.Entities;
using Fintelex_Assignment.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fintelex_Assignment.Repositories.Implements
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithProductsAsync()
        {
            return await _context.Categories.Include(c => c.Products).ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Where(c => !c.IsDeleted)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            category.IsDeleted = true; 
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
