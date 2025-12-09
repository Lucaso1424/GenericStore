using AutoMapper;
using GenericStore.Application.DTOs;
using GenericStore.Application.Interfaces;
using GenericStore.Application.Services;
using GenericStore.Domain.Entities;
using GenericStore.Domain.Enums;
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
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IMapper mapper) 
        {
            _contextAccessor = new HttpContextAccessor();
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllAsync()
        {
            var users = await userService.GetAllAsync();

            if (users == null)
            {
                return NotFound();
            }

            var usersDTO = mapper.Map<IEnumerable<UserDTO>>(users);
            foreach (var userDTO in usersDTO)
            {
                RoleId roleId = (RoleId)Enum.Parse(typeof(RoleId), userDTO.RoleId.ToString());
                userDTO.RoleDisplayName = roleId.ToString();
            }
            return Ok(usersDTO);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetByIdAsync(int id)
        {
            var user = await userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDTO = mapper.Map<UserDTO>(user);
            RoleId roleId = (RoleId)Enum.Parse(typeof(RoleId), user.RoleId.ToString());
            userDTO.RoleDisplayName = roleId.ToString();
            return Ok(userDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] UserDTO userDTO)
        {
            try
            {
                var entity = mapper.Map<User>(userDTO);
                await userService.CreateAsync(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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