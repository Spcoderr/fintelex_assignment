
using Fintelex_Assignment.Entities;

namespace Fintelex_Assignment.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Entities.Order>
    {
        Task<Entities.Order> GetOrderWithDetailsAsync(int orderId);
        Task<IEnumerable<Entities.Order>> GetOrdersByCustomerAsync(int customerId);
        Task<IEnumerable<Entities.Order>> GetOrdersByStatusAsync(string status);
        Task<Order?> GetByIdAsync(int id);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
        Task AddAsync(Order order);
    }
}
