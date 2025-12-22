using AutoMapper;
using Core.Application.Services;
using GenericStore.Application.DTOs;
using GenericStore.Application.Interfaces;
using GenericStore.Domain.Entities;
using GenericStore.Infrastructure.UnitOfWork;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GenericStore.Application.Services;
public class StoreProductService : GenericService<GenericStoreContext, StoreProduct, StoreProductDTO>, IStoreProductService
{
    public StoreProductService(GenericStoreContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreProductDTO?> GetByIdAsync(int id)
    {
        IQueryable query = _context.StoreProducts.Where(x => x.StoreProductId == id);
        var storeProduct = await query.ProjectToType<StoreProductDTO>().FirstOrDefaultAsync();
        return storeProduct;
    }
}