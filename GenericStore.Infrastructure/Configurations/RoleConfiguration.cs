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

public partial class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> entity)
    {
        entity.HasKey(e => e.RoleId).HasName("PK_ROLE_ID");

        entity.ToTable("Role");

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode(false);

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Role> entity);
}