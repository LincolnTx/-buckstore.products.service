using buckstore.products.service.application.Queries.ResponseDTOs;
using MediatR;

namespace buckstore.products.service.application.Queries
{
    public class ListProductsQuery : IRequest<ListProductResponse>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public ListProductsQuery(int pageNumber = 0, int pageSize = 0)
        {
            PageNumber = pageNumber > 0 ? (pageNumber - 1 ) * pageSize : 0;
            PageSize = pageSize > 0 ? pageSize : 10;
        }
    }
}