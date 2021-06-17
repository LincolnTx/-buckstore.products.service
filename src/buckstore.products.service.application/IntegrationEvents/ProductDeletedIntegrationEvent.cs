using System;

namespace buckstore.products.service.application.IntegrationEvents
{
    public class ProductDeletedIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }

        public ProductDeletedIntegrationEvent(DateTime timestamp) : base(timestamp)
        {
        }
    }
}