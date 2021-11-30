using System;
using System.Collections.Generic;
using System.Net;
using MediatR;
using System.Threading.Tasks;
using buckstore.products.service.api.v1.Dtos.Request;
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

        [Authorize]
        [HttpPut("evaluate")]
        public async Task<IActionResult> AddProductRate([FromBody] AddProductRateCommand productRateCommand)
        {
            var userId = Guid.Parse(GetTokenClaim("id"));
            var userName = GetTokenClaim("userName");
            productRateCommand.UserId = userId;
            productRateCommand.UserName = userName;

            var response = await _mediator.Send(productRateCommand);

            return Response(200, response);
        }

        [Authorize]
        [HttpDelete("evaluate/delete")]
        public async Task<IActionResult> DeleteProductRate([FromBody] DeleteProductRateCommand deleteProductRateCommand)
        {
            var response = await _mediator.Send(deleteProductRateCommand);

            return Response(200, response);
        }

        [Authorize]
        [HttpGet("image")]
        public async Task<IActionResult> GetImages([FromQuery] List<Guid> productId)
        {
            var response = await _mediator.Send(new FindProductImagesQuery(productId));

            return Response(200, response);
        }

        [Authorize]
        [HttpGet("favorites")]
        public async Task<IActionResult> GetUserFavorites()
        {
            var userId = Guid.Parse(GetTokenClaim("id"));
            var response = await _mediator.Send(new ListFavoritesByUserQuery { UserId = userId });

            return Response(200, response);
        }

        [Authorize]
        [HttpPost("favorites")]
        public async Task<IActionResult> AddFavorite([FromBody] FavoriteBaseRequestDto request)
        {
            var userId = Guid.Parse(GetTokenClaim("id"));
            var response = await _mediator.Send(new NewFavoriteCommand(userId, request.ProductId));

            return Response(200, response);
        }

        [Authorize]
        [HttpDelete("favorites/{productId}")]
        public async Task<IActionResult> RemoveFavorite(string productId)
        {
            var userId = Guid.Parse(GetTokenClaim("id"));
            var response = await _mediator.Send(new RemoveFavoriteCommand(userId, Guid.Parse(productId)));

            return Response(200, response);
        }
    }
}
