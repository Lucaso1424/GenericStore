using AutoMapper;
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
    public StoreService(GenericStoreContext context, IMapper mapper) : base(context, mapper)
    {

    }

    public async Task<StoreDTO?> GetByIdAsync(int id)
    {
        IQueryable<Store> query = _context.Stores
            .Include(x => x.StoreProducts)
            .Where(x => x.StoreId == id);

        var store = await query.ProjectToType<StoreDTO>().FirstOrDefaultAsync();

        return store;
    }
}