using AutoMapper;
using Core.Application.Services;
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
    public class CategoryService : GenericService<GenericStoreContext, Category, CategoryDTO>, ICategoryService
    {
        public CategoryService(GenericStoreContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(x=> x.Products)
                .Where(x=> x.CategoryId == id)
                .FirstOrDefaultAsync();
        }
    }
}