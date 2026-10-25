using Core.Application.DTOs;
using Core.Application.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Services;
public class GenericService<TContext, TEntity, TEntityDTO> : GenericServiceBase<TContext>, IGenericService<TEntity, TEntityDTO>
    where TContext : DbContext
    where TEntity : class
    where TEntityDTO : BaseEntityDTO
{
    public GenericService(TContext context) : base(context) {}

    public async Task<List<TEntityDTO>> GetAllAsync()
    {
        IQueryable query = _context.Set<TEntity>();
        var entities = await query.ProjectToType<TEntityDTO>().AsNoTracking().ToListAsync();
        return entities;
    }

    public async Task CreateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {         
        var entityToDelete = await _context.Set<TEntity>().FindAsync(id);
        if (entityToDelete != null)
        {
            _context.Set<TEntity>().Remove(entityToDelete);
            await _context.SaveChangesAsync();
        }
    }
}