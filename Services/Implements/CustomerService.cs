using AutoMapper;
using Fintelex_Assignment.Dtos;
using Fintelex_Assignment.Entities;
using Fintelex_Assignment.Repositories.Interfaces;
using Fintelex_Assignment.Services.Interfaces;

namespace Fintelex_Assignment.Services.Implements
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

      

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            return await _customerRepository.AddAsync(customer);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _customerRepository.UpdateAsync(customer);
        }

       

        

        public async Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm)
        {
            return await _customerRepository.SearchCustomersAsync(searchTerm);
        }

       
        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetCustomerWithOrdersAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerWithOrdersAsync(id);
            return _mapper.Map<CustomerDto>(customer);
        }


        public async Task<bool> UpdateCustomerAsync(CustomerDto dto)
        {
            var customer = await _customerRepository.GetByIdAsync(dto.Id);
            if (customer == null) return false;

            customer.FirstName = dto.FirstName;
            customer.LastName = dto.LastName;
            customer.Email = dto.Email;
            customer.Phone = dto.Phone;
            customer.Address = dto.Address;

            await _customerRepository.UpdateAsync(customer);
            return true;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null) return false;

            await _customerRepository.DeleteAsync(customer);
            return true;
        }
    }
}
