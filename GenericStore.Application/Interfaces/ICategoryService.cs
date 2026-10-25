using Core.Application.Interfaces;
using GenericStore.Domain.Entities;

namespace GenericStore.Application.Interfaces
{
    public interface ICategoryService : IGenericService<Category, CategoryDTO>
    {
        Task<CategoryDTO?> GetByIdAsync(int id);
        Task UpdateByIdAsync(CategoryDTO dto, int id);
    }
}