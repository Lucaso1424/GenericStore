using AutoMapper;
using Core.Application.DTOs;
using Core.Application.Interfaces;
using GenericStore.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services;
public class GenericService<TContext, TEntity, TEntityDTO> : GenericServiceBase<TContext>, IGenericService<TEntity, TEntityDTO>
    where TContext : DbContext
    where TEntity : class
    where TEntityDTO : BaseEntityDTO
{
    public GenericService(TContext context, IMapper mapper) : base(context, mapper) {}

    public async Task<IEnumerable<TEntity?>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task CreateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, BaseEntityDTO dto)
    {
        EntityState entityState = (EntityState)Enum.Parse(typeof(EntityState), dto.TrackingState.ToString());
        var entity = _mapper.Map<TEntity>(dto);

        if (entityState != EntityState.Unchanged)
            _context.Set<TEntity>().Entry(entity).State = entityState;

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