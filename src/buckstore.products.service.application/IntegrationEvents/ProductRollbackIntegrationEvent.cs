using System;
using System.Collections.Generic;

namespace buckstore.products.service.application.IntegrationEvents
{
    public class ProductRollbackIntegrationEvent : IntegrationEvent
    {
        public List<ProductsFromOrderDto> Products { get; set; }

        public ProductRollbackIntegrationEvent(List<ProductsFromOrderDto> products) : base(DateTime.Now)
        {
            Products = products;
        }
    }
}
