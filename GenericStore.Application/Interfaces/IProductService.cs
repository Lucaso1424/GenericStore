using Core.Application.Interfaces;
using GenericStore.Domain.Entities;

namespace GenericStore.Application.Interfaces
{
    public interface IProductService : IGenericService<Product, ProductDTO>
    {
        Task<ProductDTO?> GetByIdAsync(int id);
    }
}