using Core.Application.Services;
using GenericStore.Application.DTOs;
using GenericStore.Application.Interfaces;
using GenericStore.Domain.Entities;
using GenericStore.Infrastructure.UnitOfWork;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GenericStore.Application.Services
{
    public class UserService : GenericService<GenericStoreContext, User, UserDTO>, IUserService
    {
        public UserService(GenericStoreContext context) : base(context)
        {
        }

        public async Task<UserDTO?> GetUserByIdAsync(int id)
        {
            IQueryable<User> query = _context.Users
                .Include(x => x.Orders)
                .Where(x => x.UserId == id);

            var user = await query.ProjectToType<UserDTO>().AsNoTracking().FirstOrDefaultAsync();

            return user;
        }


        public async Task UpdateUserById(UserDTO userDTO, int id)
        {
            var user = await _context.Users.FindAsync(id) ?? throw new KeyNotFoundException($"User with id {id} not found");

            try
            {
                user.Name = userDTO.Name;
                user.LastName = userDTO.LastName;
                user.Email = userDTO.Email;
                user.RoleId = userDTO.RoleId;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the product.", ex);
            }
        }
    }
}