namespace Fintelex_Assignment.Repositories.Interfaces
{
    public interface IOrderItemRepository : IRepository<Entities.OrderItem>
    {
        Task<IEnumerable<Entities.OrderItem>> GetItemsByOrderAsync(int orderId);
    }
}
