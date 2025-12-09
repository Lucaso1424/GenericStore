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

public partial class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(e => e.UserId).HasName("PK_USER_ID");

        entity.ToTable("User");

        entity.HasIndex(e => e.Email, "UQ__User__A9D1053445DCD588").IsUnique();

        entity.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(200)
            .IsUnicode(false);
        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.PasswordHash)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.HasOne(d => d.Role).WithMany(p => p.Users)
            .HasForeignKey(d => d.RoleId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_User_Role");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<User> entity);
}