using Fintelex_Assignment.Dtos;
using Fintelex_Assignment.Entities;

namespace Fintelex_Assignment.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
       
        Task<Order> GetOrderWithDetailsAsync(int orderId);
        Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId);
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status);
        Task<IEnumerable<Order>> GetPagedOrdersAsync(int page, int pageSize);
        Task<bool> UpdateOrderAsync(OrderDto dto);
        Task<bool> DeleteOrderAsync(int id);
        Task<bool> CreateOrderAsync(OrderDto dto);
    }
}
