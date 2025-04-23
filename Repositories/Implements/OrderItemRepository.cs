using Fintelex_Assignment.Dbcontext;
using Fintelex_Assignment.Entities;
using Fintelex_Assignment.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fintelex_Assignment.Repositories.Implements
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<OrderItem>> GetItemsByOrderAsync(int orderId)
        {
            return await _context.OrderItems
                .Include(oi => oi.Product)
                .Where(oi => oi.OrderId == orderId)
                .ToListAsync();
        }
    }
}
