using System;
using System.Collections.Generic;

namespace buckstore.products.service.application.IntegrationEvents
{
    public class OrderReceivedIntegrationEvent : IntegrationEvent
    {
        public IEnumerable<ProductsFromOrderDto> OrderProducts { get; set; }

        public OrderReceivedIntegrationEvent(DateTime timestamp) : base(timestamp)
        {
        }
    }

    public class ProductsFromOrderDto
    {
        public Guid ProductId { get; set; }
        public int QuantitySold { get; set; }
    }
}