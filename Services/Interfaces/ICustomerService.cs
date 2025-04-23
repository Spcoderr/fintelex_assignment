using Fintelex_Assignment.Dtos;
using Fintelex_Assignment.Entities;

namespace Fintelex_Assignment.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto> GetCustomerWithOrdersAsync(int id);
        Task<Customer> GetCustomerByIdAsync(int id);
       Task<Customer> CreateCustomerAsync(Customer customer);
       
        Task<bool> UpdateCustomerAsync(CustomerDto dto);
        Task<bool> DeleteCustomerAsync(int id);

       
        Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm);
    }
}
