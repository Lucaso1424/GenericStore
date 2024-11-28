using AutoMapper;
using Core.Application.Services;
using GenericStore.Application.DTOs;
using GenericStore.Application.Interfaces;
using GenericStore.Domain.Entities;
using GenericStore.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStore.Application.Services
{
    public class UserService : GenericService<GenericStoreContext, User, UserDTO>, IUserService
    {
        public UserService(GenericStoreContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(x => x.Orders)
                .Where(x => x.UserId == id)
                .FirstOrDefaultAsync();
        }
    }
}