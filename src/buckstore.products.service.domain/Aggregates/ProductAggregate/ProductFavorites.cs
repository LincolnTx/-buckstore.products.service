using System;
using buckstore.products.service.domain.SeedWork;

namespace buckstore.products.service.domain.Aggregates.ProductAggregate
{
    public class ProductFavorites : Entity
    {
        public Guid UserId { get; private set; }
        public Guid ProductId { get; private set; }

        public ProductFavorites(Guid userId, Guid productId)
        {
            UserId = userId;
            ProductId = productId;
        }
    }
}
