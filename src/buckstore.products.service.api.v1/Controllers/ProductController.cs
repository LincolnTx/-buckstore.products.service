using System;
using System.Net;
using MediatR;
using System.Threading.Tasks;
using buckstore.products.service.application.Commands;
using buckstore.products.service.application.Queries;
using buckstore.products.service.application.Queries.ResponseDTOs;
using Microsoft.AspNetCore.Mvc;
using buckstore.products.service.domain.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace buckstore.products.service.api.v1.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IMediator _mediator;

        public ProductController(INotificationHandler<ExceptionNotification> notifications, IMediator mediator)
            : base(notifications)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(ListProductResponse), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> ListProducts([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var queryResponse = await _mediator.Send(new ListProductsQuery(pageNumber, pageSize));

            return Response(200, queryResponse);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProductResponseDto), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] Guid productCode)
        {
            var response = await _mediator.Send(new FindProductByIdQuery(productCode));

            return Response(200, response);
        }

        [HttpPut("evaluate")]
        [Authorize]
        public async Task<IActionResult> AddProductRate([FromBody] AddProductRateCommand productRateCommand)
        {
            var userId = Guid.Parse(GetTokenClaim("id"));
            var userName = GetTokenClaim("userName");
            productRateCommand.UserId = userId;
            productRateCommand.UserName = userName;

            var response = await _mediator.Send(productRateCommand);

            return Response(200, response);
        }

        [HttpDelete("evaluate/delete")]
        [Authorize]
        public async Task<IActionResult> DeleteProductRate([FromBody] DeleteProductRateCommand deleteProductRateCommand)
        {
            var response = await _mediator.Send(deleteProductRateCommand);

            return Response(200, response);
        }
    }
}
