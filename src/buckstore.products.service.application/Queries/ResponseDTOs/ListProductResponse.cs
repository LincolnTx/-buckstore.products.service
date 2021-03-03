using System.Collections.Generic;
using buckstore.products.service.application.Queries.ViewModels;

namespace buckstore.products.service.application.Queries.ResponseDTOs
{
    public class ListProductResponse
    {
        public  IEnumerable<ListProductsVW> Products { get; set; }

        public ListProductResponse(IEnumerable<ListProductsVW> products)
        {
            Products = products;
        }
    }
}