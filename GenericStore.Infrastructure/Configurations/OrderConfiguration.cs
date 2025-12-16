using GenericStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenericStore.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> entity)
    {
        entity.HasKey(e => e.OrderId);

        entity.ToTable("orders");

        entity.Property(e => e.OrderDate)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.Property(e => e.Status)
            .IsRequired()
            .HasMaxLength(20);

        entity.Property(e => e.Total)
            .HasPrecision(18, 2);

        entity.HasOne(d => d.User)
            .WithMany(p => p.Orders)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
