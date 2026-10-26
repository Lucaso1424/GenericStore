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
    public StoreProductService(GenericStoreContext context) : base(context)
    {
    }

    public async Task<StoreProductDTO?> GetByIdAsync(int id)
    {
        IQueryable<StoreProduct> query = _context.StoreProducts.Where(x => x.StoreProductId == id);
        var storeProduct = await query.ProjectToType<StoreProductDTO>().AsNoTracking().FirstOrDefaultAsync();
        return storeProduct;
    }

    public async Task UpdateByIdAsync(StoreProductDTO dto, int id)
    {
        var storeProduct = await _context.StoreProducts.FindAsync(id) ?? throw new KeyNotFoundException($"StoreProduct with id {id} not found.");

        try
        {
            storeProduct.ProductId = dto.ProductId;
            storeProduct.Stock = dto.Stock;
            storeProduct.MinimumStock = dto.MinimumStock;
            storeProduct.StoreId = dto.StoreId;

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the store product.", ex);
        }
    }
}