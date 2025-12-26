using Core.Application.DTOs;
namespace Core.Application.Interfaces;

public interface IGenericService<TEntity, TEntityDTO>
    where TEntityDTO : BaseEntityDTO
    where TEntity : class
{
    Task<List<TEntityDTO?>> GetAllAsync();
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(BaseEntityDTO entity);
    Task DeleteAsync(int id);
}