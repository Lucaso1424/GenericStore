using AutoMapper;
using GenericStore.Application.Interfaces;
using GenericStore.Domain.Entities;
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
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapping;
        public CategoryController(IHttpContextAccessor httpContextAccessor, ICategoryService categoryService, IMapper mapping)
        {
            _contextAccessor = httpContextAccessor;
            _categoryService = categoryService;
            _mapping = mapping;
        }

        [Authorize(Policy = "Api.Read")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllAsync()
        {
            var categories = await _categoryService.GetAllAsync();

            if (categories == null)
            {
                return NotFound();
            }

            var categoriesDTO = _mapping.Map<IEnumerable<CategoryDTO>>(categories);
            return Ok(categoriesDTO);
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

            var categoryDTO = _mapping.Map<CategoryDTO>(category);
            return Ok(categoryDTO);
        }

        [Authorize(Policy = "Api.Write")]
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                var entity = _mapping.Map<Category>(categoryDTO);
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
