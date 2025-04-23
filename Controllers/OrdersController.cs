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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IProductService productService, IMapper mapper)
        {
            _orderService = orderService;
            _productService = productService;
            _mapper = mapper;
        }

       

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderWithDetailsAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OrderDto>(order));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] OrderDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _orderService.CreateOrderAsync(dto);
            if (!result) return BadRequest("Could not create order");

            return Ok("Order created successfully");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] OrderDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _orderService.UpdateOrderAsync(dto);
            if (!result) return NotFound("Order not found");

            return Ok("Order updated successfully");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _orderService.DeleteOrderAsync(id);
            if (!result) return NotFound("Order not found");

            return Ok("Order deleted successfully");
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByCustomer(int customerId)
        {
            var orders = await _orderService.GetOrdersByCustomerAsync(customerId);
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByStatus(string status)
        {
            var orders = await _orderService.GetOrdersByStatusAsync(status);
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
        }
    }
}
