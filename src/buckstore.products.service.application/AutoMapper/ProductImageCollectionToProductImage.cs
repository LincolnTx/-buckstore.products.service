using System;
using buckstore.products.service.domain.Aggregates.ProductAggregate;

namespace buckstore.products.service.application.AutoMapper
{
    public class ProductImageCollectionToProductImage : MappingProfile
    {
        public ProductImageCollectionToProductImage()
        {
            CreateMap<ProductsImagesCollection, ProductImage>()
                .ConvertUsing(src => new ProductImage(Guid.Parse(src.ImageId), src.Image, src.ContentType));
        }
    }
}
