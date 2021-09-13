using System;
using System.Collections.Generic;

namespace buckstore.products.service.application.IntegrationEvents
{
    public class OrderRollbackIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }

        public List<ProductsFromOrderDto> Products { get; set; }

        public OrderRollbackIntegrationEvent() : base(DateTime.Now)
        {
        }
    }
}
