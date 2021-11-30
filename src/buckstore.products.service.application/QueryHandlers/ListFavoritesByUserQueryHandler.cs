using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using buckstore.products.service.application.Queries;
using buckstore.products.service.application.Queries.ViewModels;
using buckstore.products.service.application.Queries.ResponseDTOs;
using buckstore.products.service.domain.Exceptions;

namespace buckstore.products.service.application.QueryHandlers
{
    public class ListFavoritesByUserQueryHandler : QueryHandler, IRequestHandler<ListFavoritesByUserQuery, ListFavoritesResponseDto>
    {
        private readonly IMediator _bus;

        public ListFavoritesByUserQueryHandler(IMediator bus)
        {
            _bus = bus;
        }

        public async Task<ListFavoritesResponseDto> Handle(ListFavoritesByUserQuery request, CancellationToken cancellationToken)
        {
            using var dbConnection = DbConnection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            const string sqlCommand = "SELECT f.product_id, p.description, p.\"name\", p.price, " +
                                      "p.stock_quantity, pc.description as category " +
                                      "FROM (SELECT product_id from products.products_favorites pf " +
                                      "WHERE pf.user_id = @userId) f INNER JOIN products.product p " +
                                      "ON p.\"Id\" = f.product_id INNER JOIN products.product_category pc " +
                                      "ON p.\"_categoryId\" = pc.id";

            var data = await dbConnection.QueryAsync<ListFavoritesVW>(sqlCommand, new
            {
                userId = request.UserId
            });

            var listFavoritesVw = data.ToList();
            await FindImages(dbConnection, listFavoritesVw);

            return new ListFavoritesResponseDto(listFavoritesVw);
        }

        private async Task FindImages(IDbConnection dbConnection, IEnumerable<ListFavoritesVW> products)
        {
            const string sqlCommand = "SELECT  i .\"Image\",  i.\"ContentType\", i.product_id " +
                                      "FROM products.\"ProductImage\" i";

            var imagesUrls = new List<string>();
            var returnProducts = new List<ListFavoritesVW>();

            try
            {
                var data = await dbConnection.QueryAsync<ProductImagesVw>(sqlCommand);

                var productImages = data.ToList();

                foreach (var product in products)
                {
                    product.imagesUrl = new List<string>();
                    foreach (var image in productImages)
                    {
                        if (image.product_id == product.product_id)
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
