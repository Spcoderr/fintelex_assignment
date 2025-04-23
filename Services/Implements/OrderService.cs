using Fintelex_Assignment.Dtos;
using Fintelex_Assignment.Entities;
using Fintelex_Assignment.Repositories.Interfaces;
using Fintelex_Assignment.Services.Interfaces;

namespace Fintelex_Assignment.Services.Implements
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

       

        public async Task<bool> UpdateOrderAsync(OrderDto dto)
        {
            var order = await _orderRepository.GetByIdAsync(dto.Id);
            if (order == null) return false;

            order.OrderDate = dto.OrderDate;
            order.TotalAmount = dto.TotalAmount;
            order.Status = dto.Status;

            await _orderRepository.UpdateAsync(order);
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return false;

            await _orderRepository.DeleteAsync(order);
            return true;
        }

        public async Task<bool> CreateOrderAsync(OrderDto dto)
        {
            var order = new Order
            {
                OrderDate = dto.OrderDate,
                CustomerId = dto.CustomerId,
                TotalAmount = dto.TotalAmount,
                Status = dto.Status
            };

            await _orderRepository.AddAsync(order);
            return true;
        }

        public async Task<Order> GetOrderWithDetailsAsync(int orderId)
        {
            return await _orderRepository.GetOrderWithDetailsAsync(orderId);
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId)
        {
            return await _orderRepository.GetOrdersByCustomerAsync(customerId);
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status)
        {
            return await _orderRepository.GetOrdersByStatusAsync(status);
        }

        public async Task<IEnumerable<Order>> GetPagedOrdersAsync(int page, int pageSize)
        {
            return await _orderRepository.GetPagedAsync(page, pageSize);
        }
    }
}
