using buckstore.products.service.application.Queries.ResponseDTOs;
using buckstore.products.service.application.Queries.ViewModels;

namespace buckstore.products.service.application.AutoMapper
{
    public class ProductImageVwToProductsImageDto : MappingProfile
    {
        public ProductImageVwToProductsImageDto()
        {
            CreateMap<ProductImagesVw, ProductsImageDto>()
                .ConvertUsing(src => new ProductsImageDto(src.product_id.ToString(), src.Image, src.ContentType));
        }
    }
}
