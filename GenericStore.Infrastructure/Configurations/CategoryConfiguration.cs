using GenericStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace GenericStore.Infrastructure.Configurations;

public partial class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> entity)
    {
        entity.HasKey(e => e.CategoryId).HasName("PK_CATEGORY_ID");

        entity.ToTable("Category");

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode(false);

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Category> entity);
}