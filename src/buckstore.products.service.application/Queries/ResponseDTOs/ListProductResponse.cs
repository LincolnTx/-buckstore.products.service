using System.Collections.Generic;

namespace buckstore.products.service.application.Queries.ResponseDTOs
{
    public class ListProductResponse
    {
        public  IEnumerable<ProductResponseDto> Products { get; set; }

        public ListProductResponse(IEnumerable<ProductResponseDto> products)
        {
            Products = products;
        }
    }
}