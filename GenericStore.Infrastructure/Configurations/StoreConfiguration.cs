using GenericStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStore.Infrastructure.Configurations
{
    public partial class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> entity)
        {
            entity.HasKey(e => e.StoreId).HasName("PK_STORE_ID");

            entity.ToTable("Store");

            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Store> entity);
    }
}
