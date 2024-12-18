using AutoMapper;
using GenericStore.Application.DTOs;
using GenericStore.Application.Interfaces;
using GenericStore.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenericStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreProductController : ControllerBase
    {
        private readonly IStoreProductService storeProductService;
        private readonly IMapper mapper;
        public StoreProductController(IStoreProductService storeProductService, IMapper mapper)
        {
            this.storeProductService = storeProductService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreProductDTO>>> GetAllAsync()
        {
            var storeProducts = await storeProductService.GetAllAsync();

            if (storeProducts == null || storeProducts.Count() == 0) 
            { 
                return NotFound();
            }

            var storeProductsDTO = mapper.Map<IEnumerable<StoreProductDTO>>(storeProducts);
            return Ok(storeProductsDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoreProductDTO>> GetByIdAsync(int id)
        {
            var entity = await storeProductService.GetByIdAsync(id);

            if (entity == null)
            {
                return NoContent();
            }

            var storeProductDTO = mapper.Map<StoreProductDTO>(entity);

            return Ok(storeProductDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] StoreProductDTO storeProductDTO)
        {
            try
            {
                var entity = mapper.Map<StoreProduct>(storeProductDTO);

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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromQuery] StoreProductDTO storeProductDTO)
        {
            if (storeProductDTO.StoreProductId != id) 
            {
                return BadRequest();
            }
            try
            {
                await storeProductService.UpdateAsync(id, storeProductDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
