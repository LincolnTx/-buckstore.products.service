using Dapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using buckstore.products.service.application.Queries;
using buckstore.products.service.application.Queries.ResponseDTOs;

namespace buckstore.products.service.application.QueryHandlers
{
    public class ListProductsQueryHandler : QueryHandler, IRequestHandler<ListProductsQuery, ListProductResponse>
    {
        public ListProductsQueryHandler() { }

        public async Task<ListProductResponse> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {
            using (var dbConnection = DbConnection)
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                const string sqlCommand = "SELECT p.\"Id\", p.description ,p.name, p.price, p.stock_quantity, " +
                                          "pc.id \"categoryId\", pc.description category FROM manager.product p  " +
                                          "LEFT JOIN manager.product_category pc " +
                                          "ON p.\"_categoryId\" = pc.id " +
                                          "ORDER BY p.\"Id\" OFFSET @pageNumber ROWS FETCH NEXT @pageSize ROWS ONLY";

                var data = await dbConnection.QueryAsync<ProductResponseDto>(sqlCommand, new
                {
                    pageSize = request.PageSize,
                    pageNumber = request.PageNumber
                });

                return new ListProductResponse(data);
            }
        }
    }
}