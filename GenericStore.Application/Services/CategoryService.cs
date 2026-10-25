using Core.Application.Services;
using GenericStore.Application.DTOs;
using GenericStore.Application.Interfaces;
using GenericStore.Domain.Entities;
using GenericStore.Infrastructure.UnitOfWork;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GenericStore.Application.Services
{
    public class CategoryService : GenericService<GenericStoreContext, Category, CategoryDTO>, ICategoryService
    {
        public CategoryService(GenericStoreContext context) : base(context)
        {
        }

        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            IQueryable<Category> query = _context.Categories
                .Include(x => x.Products)
                .Where(x => x.CategoryId == id);

            var category = await query.ProjectToType<CategoryDTO>().AsNoTracking().FirstOrDefaultAsync();
            
            return category;
        }

        public async Task UpdateByIdAsync(CategoryDTO dto, int id)
        {
            var category = await _context.Categories.FindAsync(id) ?? throw new KeyNotFoundException($"Category with id {id} not found.");

            try
            {
                category.CategoryId = dto.CategoryId;
                category.Name = dto.Name;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the category.", ex);
            }
        }
    }
}