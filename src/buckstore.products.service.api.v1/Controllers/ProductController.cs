﻿using System;
using MediatR;
using System.Threading.Tasks;
using buckstore.products.service.application.Queries;
using Microsoft.AspNetCore.Mvc;
using buckstore.products.service.domain.Exceptions;

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
        public async Task<IActionResult> ListProducts([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var queryResponse = await _mediator.Send(new ListProductsQuery(pageNumber, pageSize));

            return Response(200, queryResponse);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid productCode)
        {
            var response = await _mediator.Send(new FindProductByIdQuery(productCode));

            return Response(200, response);
        }
    }
    
    

}