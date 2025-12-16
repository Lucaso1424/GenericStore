using GenericStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenericStore.Infrastructure.Configurations;

public class StoreProductConfiguration : IEntityTypeConfiguration<StoreProduct>
{
    public void Configure(EntityTypeBuilder<StoreProduct> entity)
    {
        entity.HasKey(e => e.StoreProductId);

        entity.ToTable("store_products");

        entity.Property(e => e.Stock).IsRequired();
        entity.Property(e => e.MinimumStock).IsRequired();

        entity.HasOne(d => d.Store)
            .WithMany(p => p.StoreProducts)
            .HasForeignKey(d => d.StoreId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(d => d.Product)
            .WithOne(p => p.StoreProduct)
            .HasForeignKey<StoreProduct>(d => d.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
