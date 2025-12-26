using AutoMapper;
using GenericStore.Application.Interfaces;
using GenericStore.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace GenericStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [Authorize(Policy = "Api.Read")]
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetAllAsync()
        {
            var products = await _service.GetAllAsync();

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [Authorize(Policy = "Api.Read")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetByIdAsync(int id)
        {
            var products = await _service.GetByIdAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [Authorize(Policy = "Api.Write")]
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] ProductDTO productDTO)
        {
            try
            {
                var entity = productDTO.Adapt<Product>();
                await _service.CreateAsync(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Policy = "Api.Write")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromQuery] ProductDTO productDTO)
        {
            if (id != productDTO.ProductId) 
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateAsync(productDTO);
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