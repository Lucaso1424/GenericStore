using Core.Application.Services;
using GenericStore.Application.DTOs;
using GenericStore.Domain.Entities;
using GenericStore.Identity.Application.Interfaces;
using GenericStore.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace GenericStore.Identity.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UtilsService _utilsService;
        private readonly GenericStoreContext _context;
        public AuthenticationService(UtilsService utilsService, GenericStoreContext context)
        {
            _utilsService = utilsService;
            _context = context;
        }

        public async Task RegisterNewUserAsync(UserDTO userDTO)
        {
            var user = new User
            {
                Email = userDTO.Email,
                Name = userDTO.Name,
                LastName = userDTO.LastName,
                RoleId = userDTO.RoleId,
                PasswordHash = _utilsService.EncryptSHA256(userDTO.Password)
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            string passwordHash = _utilsService.EncryptSHA256(password);

            var existingUser = await _context.Users
                .Where(x => x.Email == email && x.PasswordHash == passwordHash)
                .FirstOrDefaultAsync();

            if (existingUser == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            return _utilsService.GenerateJwtToken(existingUser);
        }
    }
}
