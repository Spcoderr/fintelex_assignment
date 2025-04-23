using AutoMapper;
using Fintelex_Assignment.Dtos;
using Fintelex_Assignment.Entities;
using Fintelex_Assignment.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fintelex_Assignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            var createdCustomer = await _customerService.CreateCustomerAsync(customer);

            return CreatedAtAction(
                nameof(GetCustomer),
                new { id = createdCustomer.Id }, 
                _mapper.Map<CustomerDto>(createdCustomer)
            );
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] CustomerDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _customerService.UpdateCustomerAsync(dto);
            if (!result) return NotFound("Customer not found");

            return Ok("Customer updated successfully");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerService.DeleteCustomerAsync(id);
            if (!result) return NotFound("Customer not found");

            return Ok("Customer deleted successfully");
        }
       

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> SearchCustomers([FromQuery] string term)
        {
            if (string.IsNullOrEmpty(term))
            {
                return BadRequest("Search term is required");
            }

            var customers = await _customerService.SearchCustomersAsync(term);
            return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customers));
        }
    }

}
