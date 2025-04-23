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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var createdProduct = await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, _mapper.Map<ProductDto>(createdProduct));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] ProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _productService.UpdateProductAsync(dto);
            if (!result) return NotFound("Product not found");

            return Ok("Product updated successfully");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result) return NotFound("Product not found");

            return Ok("Product deleted successfully");
        }



        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> SearchProducts([FromQuery] string term)
        {
            if (string.IsNullOrEmpty(term))
            {
                return BadRequest("Search term is required");
            }

            var products = await _productService.SearchProductsAsync(term);
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }
    }

}
