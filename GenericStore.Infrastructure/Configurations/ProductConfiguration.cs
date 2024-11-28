using GenericStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace GenericStore.Infrastructure.Configurations;

public partial class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity.HasKey(e => e.ProductId).HasName("PK_PRODUCT_ID");

        entity.ToTable("Product");

        entity.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(250)
            .IsUnicode(false);
        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(250)
            .IsUnicode(false);
        entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        entity.HasOne(d => d.Category).WithMany(d=> d.Products)
            .HasForeignKey(d => d.CategoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Product_Category");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Product> entity);
}