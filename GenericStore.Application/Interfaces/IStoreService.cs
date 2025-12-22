using Core.Application.Interfaces;
using GenericStore.Application.DTOs;
using GenericStore.Domain.Entities;

namespace GenericStore.Application.Interfaces;

public interface IStoreService : IGenericService<Store, StoreDTO>
{
    Task<StoreDTO?> GetByIdAsync(int id);
}