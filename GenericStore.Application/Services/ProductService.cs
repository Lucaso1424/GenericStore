using Core.Application.Services;
using GenericStore.Application.Interfaces;
using GenericStore.Domain.Entities;
using GenericStore.Infrastructure.UnitOfWork;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GenericStore.Application.Services;
public class ProductService : GenericService<GenericStoreContext, Product, ProductDTO>, IProductService
{
    public ProductService(GenericStoreContext context) : base(context)
    {

    }  

    public async Task<ProductDTO?> GetByIdAsync(int id)
    {
        IQueryable<Product> query = _context.Products
            .Include(x => x.StoreProduct)
            .Where(x => x.ProductId == id);

        var product = await query.ProjectToType<ProductDTO>().AsNoTracking().FirstOrDefaultAsync();

        return product;
    }

    public async Task UpdateProductAsync(ProductDTO productDto, int id)
    {
        var product = await _context.Products.FindAsync(id) ?? throw new KeyNotFoundException($"Product with id {id} not found.");

        try
        {
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.OnSale = productDto.OnSale;
            product.CategoryId = productDto.CategoryId;
            
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while updating the product.", ex);
        }
    }
}