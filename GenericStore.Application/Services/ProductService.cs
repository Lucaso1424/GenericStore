using AutoMapper;
using Core.Application.Services;
using GenericStore.Application.Interfaces;
using GenericStore.Domain.Entities;
using GenericStore.Infrastructure.UnitOfWork;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GenericStore.Application.Services;
public class ProductService : GenericService<GenericStoreContext, Product, ProductDTO>, IProductService
{
    public ProductService(GenericStoreContext context, IMapper mapper) : base(context, mapper)
    {

    }  

    public async Task<ProductDTO?> GetByIdAsync(int id)
    {
        IQueryable query = _context.Products
            .Include(x => x.StoreProduct)
            .Where(x => x.ProductId == id);

        var product = await query.ProjectToType<ProductDTO>().FirstOrDefaultAsync();

        return product;
    }
}