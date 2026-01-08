using AutoMapper;
using GenericStore.Application.DTOs;
using GenericStore.Application.Interfaces;
using GenericStore.Application.Services;
using GenericStore.Domain.Entities;
using GenericStore.Domain.Enums;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenericStore.Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService, IMapper mapper) 
        {
            this.userService = userService;
        }

        [Authorize(Policy = "Api.Read")]
        [Authorize(Policy = "Role.Admin")]
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllAsync()
        {
            var users = await userService.GetAllAsync();

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [Authorize(Policy = "Api.Read")]
        [Authorize(Policy = "Role.Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetByIdAsync(int id)
        {
            var user = await userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Authorize(Policy = "Role.Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] UserDTO userDTO)
        {
            try
            {
                var entity = userDTO.Adapt<User>();
                await userService.CreateAsync(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Policy = "Role.Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] UserDTO userDTO, int id)
        {
            try
            {
                if (userDTO.UserId != id)
                {
                    return BadRequest();
                }
                await userService.UpdateAsync(userDTO);
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
                var entityToDelete = await userService.GetUserByIdAsync(id);
                if (entityToDelete == null)
                    return NotFound();

                await userService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}