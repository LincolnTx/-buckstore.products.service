using buckstore.products.service.application.Queries.ResponseDTOs;
using buckstore.products.service.application.Queries.ViewModels;
using buckstore.products.service.domain.Aggregates.ProductAggregate;

namespace buckstore.products.service.application.AutoMapper
{
    public class FindProductVwToProductRateDto : MappingProfile
    {
        public FindProductVwToProductRateDto()
        {
            CreateProductRate();
        }

        public void CreateProductRate()
        {
            CreateMap<FindProductWithRateVW, ProductResponseDto>()
                .ConstructUsing(c => new ProductResponseDto(c));
        }
    }
}