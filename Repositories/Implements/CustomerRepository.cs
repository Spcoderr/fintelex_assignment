using Fintelex_Assignment.Dbcontext;
using Fintelex_Assignment.Entities;
using Fintelex_Assignment.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fintelex_Assignment.Repositories.Implements
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Customer> GetCustomerWithOrdersAsync(int customerId)
        {
            return await _context.Customers
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == customerId);
        }

        public async Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm)
        {
            return await _context.Customers
                .Where(c => c.FirstName.Contains(searchTerm) ||
                           c.LastName.Contains(searchTerm) ||
                           c.Email.Contains(searchTerm))
                .ToListAsync();
        }
        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers
                .Where(c => !c.IsDeleted)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Customer customer)
        {
            customer.IsDeleted = true; // ✅ Soft delete
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }
    }
}
