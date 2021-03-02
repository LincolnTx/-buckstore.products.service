using buckstore.products.service.domain.Aggregates.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace buckstore.products.service.infrastructure.Data.Mappings.Database
{
    public class ProductCategoryMap : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("product_category");

            builder.HasKey(category => category.Id);

            builder.Property(category => category.Id)
                .IsRequired()
                .HasColumnName("id");

            builder.Property(category => category.Name)
                .HasMaxLength(80)
                .IsRequired()
                .HasColumnName("description");

            builder.HasData(ProductCategory.Gamer, ProductCategory.SmartPhones, ProductCategory.Pc,
                ProductCategory.Gadgets, ProductCategory.Hardware, ProductCategory.Office);
        }
    }
}