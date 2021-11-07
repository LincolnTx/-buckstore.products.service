using System;
using Dapper;
using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using buckstore.products.service.domain.Exceptions;
using buckstore.products.service.application.Queries;
using buckstore.products.service.application.Queries.ViewModels;
using buckstore.products.service.application.Queries.ResponseDTOs;

namespace buckstore.products.service.application.QueryHandlers
{
    public class FindProductImagesQueryHandler : QueryHandler, IRequestHandler<FindProductImagesQuery, IEnumerable<ProductsImageDto>>
    {
        private readonly IMediator _bus;
        private readonly IMapper _mapper;

        public FindProductImagesQueryHandler(IMediator bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductsImageDto>> Handle(FindProductImagesQuery request, CancellationToken cancellationToken)
        {
            using var dbConnection = DbConnection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            const string sqlCommand =
                "SELECT DISTINCT ON (p2.product_id) p2.product_id, p2.\"Image\", p2.\"ContentType\" " +
                "FROM products.product p LEFT JOIN products.\"ProductImage\" p2 " +
                "ON p.\"Id\" = any (@ids)";

            try
            {
                var data = await dbConnection.QueryAsync<ProductImagesVw>(sqlCommand, new
                {
                    ids = request.ProductIds.ToArray()
                });

                return BuildResponse(data);
            }
            catch (Exception e)
            {
                await _bus.Publish(
                    new ExceptionNotification("010",
                        "Não foi possível encontrar imagens para essess produtos"),
                    cancellationToken);

                return default;
            }
        }

        public IEnumerable<ProductsImageDto> BuildResponse(IEnumerable<ProductImagesVw> resultData)
        {
            var productsImages = new List<ProductsImageDto>();

            foreach (var item in resultData)
            {
                if (item.ContentType !=  null && item.Image != null)
                {
                    var productImage = _mapper.Map<ProductsImageDto>(item);
                    productsImages.Add(productImage);
                }
            }

            return productsImages;
        }
    }
}
