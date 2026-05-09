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

            var user = await query.ProjectToType<UserDTO>().FirstOrDefaultAsync();

            return user;
        }
    }
}