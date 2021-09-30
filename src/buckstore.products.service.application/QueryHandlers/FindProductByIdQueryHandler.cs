using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Dapper;
using MediatR;
using AutoMapper;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using buckstore.products.service.domain.Exceptions;
using buckstore.products.service.application.Queries;
using buckstore.products.service.application.Queries.ResponseDTOs;
using buckstore.products.service.application.Queries.ViewModels;

namespace buckstore.products.service.application.QueryHandlers
{
    public class FindProductByIdQueryHandler : QueryHandler, IRequestHandler<FindProductByIdQuery, ProductResponseDto>
    {
        private readonly IMediator _bus;
        private readonly IMapper _mapper;

        public FindProductByIdQueryHandler(IMediator bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        public async Task<ProductResponseDto> Handle(FindProductByIdQuery request, CancellationToken cancellationToken)
        {
            using var dbConnection = DbConnection;
            double averageRate = 0;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var sqlCommand = BuildSqlCommand();

            try
            {
                var images = await FindImages(dbConnection, request.ProductCode);
                var data = await dbConnection.QueryAsync<FindProductWithRateVW>(sqlCommand, new
                {
                    productCode = request.ProductCode
                });

                var findProductWithRateVws = data.ToList();
                var product = _mapper.Map<ProductResponseDto>(findProductWithRateVws.First());
                product.SetImagesUrl(images);
                if (findProductWithRateVws.ToList().First().RateId == Guid.Empty)
                {
                    product.AverageRate = new decimal(0);
                    return product;
                }
                foreach (var item in findProductWithRateVws.ToList())
                {
                    product.MergeRate(item.RateId,item.RateValue, item.Comment, item.UserName);
                    averageRate += item.RateValue;
                }

                product.AverageRate = new decimal(averageRate / findProductWithRateVws.ToList().Count);

                return product;
            }
            catch (Exception e)
            {
                await _bus.Publish(new ExceptionNotification("002",
                    "Produto não encontrado. É possível que o código de produto seja inválido",
                    "productCode"), CancellationToken.None);

                return null;
            }
        }

        private static string BuildSqlCommand()
        {
            var command = new StringBuilder("select p.\"Id\" , p.name, p.description , p.price , p.stock_quantity ,");
            command.Append("pc.id \"_categoryId\", pc.description as category, pr.\"RateValue\", pr.\"Comment\", ");
            command.Append("pr.\"Id\" as RateId, pr.\"UserName\" ");
            command.Append("from products.product p ");
            command.Append("left join products.product_category pc on p.\"_categoryId\" = pc.id ");
            command.Append("left JOIN products.\"ProductRate\" pr on pr.product_id = p.\"Id\" ");
            command.Append("where p.\"Id\" = @productCode");

            return command.ToString();
        }

        private async Task<IEnumerable<string>> FindImages(IDbConnection dbConnection, Guid code)
        {
            const string sqlCommand = "SELECT  i .\"Image\",  i.\"ContentType\" " +
                "FROM products.\"ProductImage\" i WHERE i.product_id = @productCode";
            var imagesUrls = new List<string>();

            try
            {
                var data = await dbConnection.QueryAsync<ProductImagesVw>(sqlCommand, new
                {
                    productCode = code
                });

                var productImages = data.ToList();
                if (productImages.Count == 0)
                {
                    return imagesUrls;
                }

                foreach (var image in productImages)
                {
                    var base64 = Convert.ToBase64String(image.Image, 0, image.Image.Length);
                    var urlImage = $"data:{image.ContentType};base64,{base64}";
                    imagesUrls.Add(urlImage);
                }
            }
            catch (Exception e)
            {

                await _bus.Publish(new ExceptionNotification("002",
                    "Produto não encontrado. É possível que o código de produto seja inválido",
                    "productCode"), CancellationToken.None);

                return null;
            }

            return imagesUrls;
        }
    }
}
