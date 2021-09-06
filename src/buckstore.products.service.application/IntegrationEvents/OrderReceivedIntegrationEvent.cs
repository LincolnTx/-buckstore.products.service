using System;
using System.Collections.Generic;

namespace buckstore.products.service.application.IntegrationEvents
{
    public class OrderReceivedIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }

        public IEnumerable<ProductsFromOrderDto> OrderProducts { get; set; }

        public OrderReceivedIntegrationEvent(DateTime timestamp) : base(timestamp)
        {
        }
    }

    public class ProductsFromOrderDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
