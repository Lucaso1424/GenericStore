using GenericStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenericStore.Infrastructure.Configurations;
public partial class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> entity)
    {
        entity.HasKey(e => e.OrderDetailId).HasName("PK_ORDERDETAIL_ID");
        entity.ToTable("OrderDetail");
        entity.Property(e => e.OrderId).HasColumnType("int");
        entity.Property(e => e.ProductId).HasColumnType("int");
        entity.Property(e => e.Quantity).HasColumnType("int");
        entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

        entity.HasOne(d => d.Order).WithOne(p => p.OrderDetail)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_OrderDetail_Order");

        entity.HasOne(d => d.Product).WithOne(p => p.OrderDetail)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_OrderDetail_Product");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<OrderDetail> entity);
}