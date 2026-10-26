using Core.Application.Interfaces;
using GenericStore.Application.DTOs;
using GenericStore.Domain.Entities;

namespace GenericStore.Application.Interfaces;

public interface IStoreService : IGenericService<Store, StoreDTO>
{
    public Task<StoreDTO?> GetByIdAsync(int id);
    public Task UpdateByIdAsync(StoreDTO dto, int id);
}