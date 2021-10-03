using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using buckstore.products.service.application.Queries;
using buckstore.products.service.application.Queries.ResponseDTOs;
using buckstore.products.service.application.Queries.ViewModels;
using buckstore.products.service.domain.Exceptions;

namespace buckstore.products.service.application.QueryHandlers
{
    public class ListProductsQueryHandler : QueryHandler, IRequestHandler<ListProductsQuery, ListProductResponse>
    {
        private readonly IMediator _bus;
        public ListProductsQueryHandler(IMediator bus)
        {
            _bus = bus;
        }

        public async Task<ListProductResponse> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {
            using var dbConnection = DbConnection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            const string sqlCommand = "SELECT p.\"Id\", p.description ,p.name, p.price, p.stock_quantity, c.number_of_products, " +
                                      "pc.id \"categoryId\", pc.description category " +
                                      "FROM (select count(p.\"Id\") number_of_products from products.product p) c, products.product p  " +
                                      "LEFT JOIN products.product_category pc " +
                                      "ON p.\"_categoryId\" = pc.id " +
                                      "ORDER BY p.\"Id\" OFFSET @pageNumber ROWS FETCH NEXT @pageSize ROWS ONLY";

            var data = await dbConnection.QueryAsync<ListProductsVW>(sqlCommand, new
            {
                pageSize = request.PageSize,
                pageNumber = request.PageNumber
            });

            var listProductsVws = data.ToList();
            await FindImages(dbConnection, listProductsVws);
            return new ListProductResponse(listProductsVws, request.PageSize, listProductsVws[0].number_of_products);
        }

        private async Task FindImages(IDbConnection dbConnection, IEnumerable<ListProductsVW> products)
        {
            const string sqlCommand = "SELECT  i .\"Image\",  i.\"ContentType\", i.product_id " +
                                      "FROM products.\"ProductImage\" i";

            var imagesUrls = new List<string>();
            var returnProducts = new List<ListProductsVW>();

            try
            {
                var data = await dbConnection.QueryAsync<ProductImagesVw>(sqlCommand);

                var productImages = data.ToList();

                foreach (var product in products)
                {
                    product.imagesUrl = new List<string>();
                    foreach (var image in productImages)
                    {
                        if (image.product_id == product.Id)
                        {
                            var base64 = Convert.ToBase64String(image.Image, 0, image.Image.Length);
                            var urlImage = $"data:{image.ContentType};base64,{base64}";
                            product.imagesUrl.Add(urlImage);
                        }
                    }
                }
            }
            catch (Exception e)
            {

                await _bus.Publish(new ExceptionNotification("002",
                    "Produto não encontrado. É possível que o código de produto seja inválido",
                    "productCode"), CancellationToken.None);

            }

        }
    }
}
