using GenericStore.Domain.Entities;

namespace GenericStore.Infrastructure.Persistence.Interfaces;

public interface IProductRepository
{
    public Task<Product?> GetByIdAsync(int id);
}