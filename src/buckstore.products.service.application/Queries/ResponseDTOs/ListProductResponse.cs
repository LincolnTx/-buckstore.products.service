using System.Collections.Generic;
using buckstore.products.service.application.Queries.ViewModels;

namespace buckstore.products.service.application.Queries.ResponseDTOs
{
    public class ListProductResponse
    {
        public  IEnumerable<ListProductsVW> Products { get; set; }
        public double TotalPages { get; set; }

        public ListProductResponse(IEnumerable<ListProductsVW> products, int pageSize, double totalProducts)
        {
            Products = products;
            TotalPages = totalProducts / pageSize <= 1 ? 1 : totalProducts / pageSize;
        }
    }
}
