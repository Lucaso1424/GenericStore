using Core.Application.Services;
using GenericStore.Application.DTOs;
using GenericStore.Application.Interfaces;
using GenericStore.Domain.Entities;
using GenericStore.Infrastructure.UnitOfWork;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GenericStore.Application.Services;

public class StoreService : GenericService<GenericStoreContext, Store, StoreDTO>, IStoreService
{
    public StoreService(GenericStoreContext context) : base(context)
    {

    }

    public async Task<StoreDTO?> GetByIdAsync(int id)
    {
        IQueryable<Store> query = _context.Stores
            .Include(x => x.StoreProducts)
            .Where(x => x.StoreId == id);

        var store = await query.ProjectToType<StoreDTO>().AsNoTracking().FirstOrDefaultAsync();

        return store;
    }

    public async Task UpdateByIdAsync(StoreDTO dto, int id)
    {
        var product = await _context.Stores.FindAsync(id);

        if (product is null)
        {
            throw new KeyNotFoundException($"Store with id {id} not found.");
        }

        try
        {
            product.StoreId = dto.StoreId;
            product.Name = dto.Name;
            product.Location = dto.Location;

            await _context.SaveChangesAsync();
        }
        catch (Exception ex) 
        { 
            throw new Exception("An error occurred while updating the store.", ex);
        }
    }
}