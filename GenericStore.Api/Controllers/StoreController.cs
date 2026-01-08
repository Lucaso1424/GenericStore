using AutoMapper;
using GenericStore.Application.DTOs;
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
    public class StoreController : ControllerBase
    {
        private readonly IStoreService storeService;
        public StoreController(IStoreService storeService)
        {
            this.storeService = storeService;
        }

        [Authorize(Policy = "Api.Read")]
        [HttpGet]
        public async Task<ActionResult<List<StoreDTO>>> GetAllAsync()
        {
            var stores = await storeService.GetAllAsync();

            if (stores == null)
            {
                return NotFound();
            }

            return Ok(stores);
        }

        [Authorize(Policy = "Api.Read")]
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreDTO>> GetByIdAsync(int id)
        {
            var store = await storeService.GetByIdAsync(id);

            if (store == null)
            {
                return NotFound();
            }

            return Ok(store);
        }

        [Authorize(Policy = "Role.Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] StoreDTO storeDTO)
        {
            try
            {
                var entity = storeDTO.Adapt<Store>();
                await storeService.CreateAsync(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Policy = "Role.Admin")]
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

        [Authorize(Policy = "Role.Admin")]
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
