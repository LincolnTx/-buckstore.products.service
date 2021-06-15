using System;
using Dapper;
using MediatR;
using AutoMapper;
using System.Linq;
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
            using (var dbConnection = DbConnection)
            {
                double averageRate = 0;
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                const string sqlCommand = "SELECT p.\"Id\", p.description ,p.name, p.price, p.stock_quantity, " +
                                          "pc.id \"categoryId\", pc.description category, "+
                                          "pr.\"RateValue\", pr.\"Comment\", u.\"name\" username, u.surname, " +
                                          "pr.\"Id\" as RateId FROM products.product p " +
                                          "INNER JOIN products.\"ProductRate\" pr on pr.product_id = p.\"Id\" " +
                                          "LEFT JOIN auth.\"User\" u on u.\"Id\" = pr.\"UserId\" " +
                                          "LEFT JOIN products.product_category pc " +
                                          "ON p.\"_categoryId\" = pc.id  WHERE p.\"Id\" = @productCode";

                try
                {
                    var data = await dbConnection.QueryAsync<FindProductWithRateVW>(sqlCommand, new
                    {
                        productCode = request.ProductCode
                    });

                    var findProductWithRateVws = data.ToList();
                    var product = _mapper.Map<ProductResponseDto>(findProductWithRateVws.First());
                    foreach (var item in findProductWithRateVws.ToList())
                    {
                        product.MergeRate(item.RateId,item.RateValue, item.Comment, item.username, item.surname);
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
        }
    }
}