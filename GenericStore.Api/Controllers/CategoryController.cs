using AutoMapper;
using GenericStore.Application.Interfaces;
using GenericStore.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenericStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapping;
        public CategoryController(ICategoryService categoryService, IMapper mapping)
        {
            _categoryService = categoryService;
            _mapping = mapping;
        }

        [Authorize(Policy = "Api.Read")]
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAllAsync()
        {
            var categories = await _categoryService.GetAllAsync();

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        [Authorize(Policy = "Api.Read")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetByIdAsync(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [Authorize(Policy = "Api.Write")]
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                var entity = categoryDTO.Adapt<Category>();
                await _categoryService.CreateAsync(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Policy = "Api.Write")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromQuery] CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.CategoryId)
            {
                return BadRequest();
            }

            try
            {
                await _categoryService.UpdateAsync(categoryDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Policy = "Api.Write")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var entityToDelete = await _categoryService.GetByIdAsync(id);

            if (entityToDelete == null)
            {
                return NotFound();
            }

            try
            {
                await _categoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
