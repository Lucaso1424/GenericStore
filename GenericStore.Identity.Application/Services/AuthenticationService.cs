using Core.Application.Services;
using GenericStore.Application.DTOs;
using GenericStore.Domain.Entities;
using GenericStore.Identity.Application.Interfaces;
using GenericStore.Infrastructure.UnitOfWork;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;

namespace GenericStore.Identity.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UtilsService _utilsService;
        private readonly GenericStoreContext _context;
        private readonly ILogger<AuthenticationService> _logger;
        public AuthenticationService(UtilsService utilsService, GenericStoreContext context, ILogger<AuthenticationService> logger)
        {
            _utilsService = utilsService;
            _context = context;
            _logger = logger;
        }

        public async Task RegisterNewUserAsync(UserDTO userDTO)
        {
            var user = userDTO.Adapt<User>();
            user.PasswordHash = _utilsService.EncryptSHA256(userDTO.Password);
            await _context.Users.AddAsync(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Register attempt for {Email}", user.Email);
                throw new DuplicateNameException("There is already a user with the same email address.", ex);
            }
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            string passwordHash = _utilsService.EncryptSHA256(password);

            IQueryable query = _context.Users
                .Where(x => x.Email == email && x.PasswordHash == passwordHash);

            var existingUser = await query.ProjectToType<User>().FirstOrDefaultAsync();

            if (existingUser == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            return _utilsService.GenerateJwtToken(existingUser);
        }
    }
}
