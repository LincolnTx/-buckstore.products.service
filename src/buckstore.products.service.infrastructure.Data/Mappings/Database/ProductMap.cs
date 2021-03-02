using buckstore.products.service.domain.Aggregates.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace buckstore.products.service.infrastructure.Data.Mappings.Database
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product");

            builder.HasKey(product => product.Id);

            builder.Property(product => product.Name)
                .HasField("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(product => product.Description)
                .HasField("_description")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasMaxLength(300)
                .HasColumnName("description");

            builder.Property(product => product.Price)
                .HasField("_price")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("price")
                .IsRequired();

            builder.Property(product => product.Stock)
                .HasField("_stockQuantity")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("stock_quantity")
                .IsRequired();

            builder.HasOne(product => product.Category)
                .WithMany()
                .IsRequired()
                .HasForeignKey("_categoryId")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(product => product.Id)
                .IsUnique();
        }
    }
}