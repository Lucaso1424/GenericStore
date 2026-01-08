using AutoMapper;
using GenericStore.Application.DTOs;
using GenericStore.Domain.Entities;
using GenericStore.Identity.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GenericStore.Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("RegisterUser")]
        [Authorize(Policy = "Role.Admin")]
        public async Task<ActionResult> RegisterUserAsync([FromBody] UserDTO userDTO)
        {
            await _authService.RegisterNewUserAsync(userDTO);
            return NoContent();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync([FromBody] UserDTO userDTO)
        {
            var token = await _authService.LoginAsync(userDTO.Email, userDTO.Password);
            return Ok(token);
        }
    }
}
