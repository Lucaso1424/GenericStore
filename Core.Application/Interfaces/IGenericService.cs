using Core.Application.DTOs;
using GenericStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces;

public interface IGenericService<TEntity, TEntityDTO>
    where TEntityDTO : BaseEntityDTO
    where TEntity : class
{
    Task<IEnumerable<TEntity?>> GetAllAsync();
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(BaseEntityDTO entity);
    Task DeleteAsync(int id);
}