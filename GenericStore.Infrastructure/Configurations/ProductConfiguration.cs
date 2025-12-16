using GenericStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace GenericStore.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity.HasKey(e => e.ProductId);

        entity.ToTable("products");

        entity.Property(e => e.CreatedDate)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(250);

        entity.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(250);

        entity.Property(e => e.Price)
            .HasPrecision(18, 2);

        entity.HasOne(d => d.Category)
            .WithMany(p => p.Products)
            .HasForeignKey(d => d.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
