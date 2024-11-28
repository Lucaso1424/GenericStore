using GenericStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenericStore.Infrastructure.Configurations;

public partial class StoreProductConfiguration : IEntityTypeConfiguration<StoreProduct>
{
    public void Configure(EntityTypeBuilder<StoreProduct> entity)
    {
        entity.HasKey(e => e.StoreProductId).HasName("PK_StoreProduct_Id");

        entity.ToTable("StoreProduct");
        entity.Property(e => e.MinimumStock).HasColumnType("int");
        entity.Property(e => e.Stock).HasColumnType("int");
        entity.Property(e => e.StoreId).HasColumnType("int");
        entity.Property(e => e.ProductId).HasColumnType("int");
        entity.HasOne(d => d.Store).WithMany(d => d.StoreProducts)
            .HasForeignKey(d => d.StoreId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_StoreProduct_Store");

        entity.HasOne(d => d.Product).WithOne(d=> d.StoreProduct)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_StoreProduct_Product");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<StoreProduct> entity);
}