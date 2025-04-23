using Fintelex_Assignment.Entities;

namespace Fintelex_Assignment.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepository<Entities.Customer>
    {
        Task<IEnumerable<Entities.Customer>> SearchCustomersAsync(string searchTerm);
        Task<Entities.Customer> GetCustomerWithOrdersAsync(int customerId);
        Task<Customer?> GetByIdAsync(int id);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Customer customer);
    }
}
