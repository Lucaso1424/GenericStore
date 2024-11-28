using AutoMapper;
using GenericStore.Application.Interfaces;
using GenericStore.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenericStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapping;

        public ProductController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapping = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllAsync()
        {
            var products = await _service.GetAllAsync();

            if (products == null)
            {
                return NotFound();
            }

            var productsDTO = _mapping.Map<IEnumerable<ProductDTO>>(products);
            return Ok(productsDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetByIdAsync(int id)
        {
            var products = await _service.GetByIdAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            var productDTO = _mapping.Map<ProductDTO>(products);
            return Ok(productDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] ProductDTO productDTO)
        {
            try
            {
                var entity = _mapping.Map<Product>(productDTO);
                await _service.CreateAsync(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromQuery] ProductDTO productDTO)
        {
            if (id != productDTO.ProductId) 
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateAsync(id, productDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var entityToDelete = await _service.GetByIdAsync(id);

            if (entityToDelete == null)
            {
                return NotFound();
            }

            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}