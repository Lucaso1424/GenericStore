using GenericStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace GenericStore.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(e => e.UserId);

        entity.ToTable("users");

        entity.HasIndex(e => e.Email)
            .IsUnique();

        entity.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

        entity.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(200);

        entity.Property(e => e.PasswordHash)
            .IsRequired()
            .HasMaxLength(200);

        entity.HasOne(d => d.Role)
            .WithMany(p => p.Users)
            .HasForeignKey(d => d.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}