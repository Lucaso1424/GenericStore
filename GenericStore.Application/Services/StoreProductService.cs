using AutoMapper;
using Core.Application.Services;
using GenericStore.Application.DTOs;
using GenericStore.Domain.Entities;
using GenericStore.Infrastructure.UnitOfWork;

namespace GenericStore.Application.Services;
public class StoreProductService : GenericService<GenericStoreContext, StoreProduct, StoreProductDTO>
{
    public StoreProductService(GenericStoreContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreProduct?> GetByIdAsync(int id)
    {
        return await _context.StoreProducts.FindAsync(id);
    }
}