using Microsoft.EntityFrameworkCore;
using buckstore.products.service.domain.Aggregates.ProductAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace buckstore.products.service.infrastructure.Data.Mappings.Database
{
    public class ProductFavoriteMap : IEntityTypeConfiguration<ProductFavorites>
    {
        public void Configure(EntityTypeBuilder<ProductFavorites> builder)
        {
            builder.ToTable("products_favorites");

            builder.HasKey(favorite => new { favorite.ProductId, favorite.UserId });

            builder.Property(favorite => favorite.ProductId)
                .IsRequired()
                .HasColumnName("product_id");

            builder.Property(favorite => favorite.UserId)
                .IsRequired()
                .HasColumnName("user_id");
        }
    }
}
