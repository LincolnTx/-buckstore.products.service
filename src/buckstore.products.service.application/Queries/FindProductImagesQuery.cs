using System;
using System.Collections.Generic;
using buckstore.products.service.application.Queries.ResponseDTOs;
using MediatR;

namespace buckstore.products.service.application.Queries
{
    public class FindProductImagesQuery : IRequest<IEnumerable<ProductsImageDto>>
    {
        public List<Guid> ProductIds { get; set; }

        public FindProductImagesQuery(List<Guid> productIds)
        {
            ProductIds = productIds;
        }
    }
}
