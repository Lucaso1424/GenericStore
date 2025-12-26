using AutoMapper;
using GenericStore.Application.DTOs;
using GenericStore.Application.Interfaces;
using GenericStore.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GenericStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoreProductController : ControllerBase
    {
        private readonly IStoreProductService storeProductService;
        public StoreProductController(IStoreProductService storeProductService)
        {
            this.storeProductService = storeProductService;
        }

        [Authorize(Policy = "Api.Read")]
        [HttpGet]
        public async Task<ActionResult<List<StoreProductDTO>>> GetAllAsync()
        {
            var storeProducts = await storeProductService.GetAllAsync();

            if (storeProducts == null || storeProducts.Count() == 0) 
            { 
                return NotFound();
            }

            return Ok(storeProducts);
        }

        [Authorize(Policy = "Api.Read")]
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreProductDTO>> GetByIdAsync(int id)
        {
            var storeProduct = await storeProductService.GetByIdAsync(id);

            if (storeProduct == null)
            {
                return NoContent();
            }

            return Ok(storeProduct);
        }

        [Authorize(Policy = "Api.Write")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] StoreProductDTO storeProductDTO)
        {
            try
            {
                var entity = storeProductDTO.Adapt<StoreProduct>();

                if (entity == null)
                {
                    return BadRequest();
                }

                await storeProductService.CreateAsync(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Policy = "Api.Write")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromQuery] StoreProductDTO storeProductDTO)
        {
            if (storeProductDTO.StoreProductId != id) 
            {
                return BadRequest();
            }
            try
            {
                await storeProductService.UpdateAsync(storeProductDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
