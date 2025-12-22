using AutoMapper;
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
    public GenericService(TContext context, IMapper mapper) : base(context, mapper) {}

    public async Task<List<TEntityDTO?>> GetAllAsync()
    {
        IQueryable query = _context.Set<TEntity>();
        var entities = await query.ProjectToType<TEntityDTO?>().ToListAsync();
        return entities;
    }

    public async Task CreateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(BaseEntityDTO dto)
    {
        if (!Enum.TryParse(dto.TrackingState.ToString(), out EntityState entityState))
            throw new ArgumentException("Invalid TrackingState value");

        var entity = _mapper.Map<TEntity>(dto);
        if (entity == null || entityState == EntityState.Unchanged) return;

        switch (entityState)
        {
            case EntityState.Added:
                _context.Set<TEntity>().Add(entity);
                break;
            case EntityState.Modified:
                _context.Set<TEntity>().Update(entity);
                break;
            case EntityState.Deleted:
                _context.Set<TEntity>().Remove(entity);
                break;
            default:
                _context.Set<TEntity>().Attach(entity);
                _context.Entry(entity).State = entityState;
                break;
        }

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