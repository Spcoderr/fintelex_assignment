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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriesController> _logger; 

        public CategoriesController(ICategoryService categoryService, IMapper mapper, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _logger = logger; 
        }





        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            var createdCategory = await _categoryService.CreateCategoryAsync(category);

            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, _mapper.Map<CategoryDto>(createdCategory));
        }



        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] CategoryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _categoryService.UpdateCategoryAsync(dto);
            if (!result) return NotFound("Category not found");

            return Ok("Category updated successfully");
        }

        
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result) return NotFound("Category not found");

            return Ok("Category deleted successfully");
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(_mapper.Map<CategoryDto>(category));
        }




        [HttpGet("with-products")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesWithProducts()
        {
            var categories = await _categoryService.GetCategoriesWithProductsAsync();
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }

       
    }

}
