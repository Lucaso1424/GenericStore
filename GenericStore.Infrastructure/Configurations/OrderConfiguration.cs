using GenericStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
namespace GenericStore.Infrastructure.Configurations;

public partial class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> entity)
    {
        entity.HasKey(e => e.OrderId).HasName("PK_ORDER_ID");

        entity.ToTable("Order");

        entity.Property(e => e.OrderDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.Status)
            .IsRequired()
            .HasMaxLength(20)
            .IsUnicode(false);
        entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

        entity.HasOne(d => d.User).WithMany(p => p.Orders)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Order_User");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Order> entity);
}