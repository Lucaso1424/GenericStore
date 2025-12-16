using GenericStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenericStore.Infrastructure.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> entity)
    {
        entity.HasKey(e => e.OrderDetailId);

        entity.ToTable("order_details");

        entity.Property(e => e.Quantity)
            .IsRequired();

        entity.Property(e => e.Price)
            .HasPrecision(18, 2);

        entity.HasOne(d => d.Order)
            .WithOne(p => p.OrderDetail)
            .HasForeignKey<OrderDetail>(d => d.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(d => d.Product)
            .WithOne(p => p.OrderDetail)
            .HasForeignKey<OrderDetail>(d => d.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
