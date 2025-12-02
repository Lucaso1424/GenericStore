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
    public class StoreController : ControllerBase
    {
        private readonly IStoreService storeService;
        private readonly IMapper mapper;
        public StoreController(IStoreService storeService, IMapper mapper)
        {
            this.storeService = storeService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreDTO>>> GetAllAsync()
        {
            var stores = await storeService.GetAllAsync();

            if (stores == null)
            {
                return NotFound();
            }

            var storesDTO = mapper.Map<IEnumerable<StoreDTO>>(stores);
            return Ok(storesDTO);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<StoreDTO>> GetByIdAsync(int id)
        {
            var store = await storeService.GetByIdAsync(id);

            if (store == null)
            {
                return NotFound();
            }

            var storeDTO = mapper.Map<StoreDTO>(store);
            return Ok(storeDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] StoreDTO storeDTO)
        {
            try
            {
                var entity = mapper.Map<Store>(storeDTO);
                await storeService.CreateAsync(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] StoreDTO storeDTO, int id)
        {
            try
            {
                if (storeDTO.StoreId != id)
                {
                    return BadRequest();
                }
                await storeService.UpdateAsync(storeDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not update: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id) 
        {
            try 
            {
                var entityToDelete = await storeService.GetByIdAsync(id);
                if (entityToDelete == null)
                    return NotFound();

                await storeService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
