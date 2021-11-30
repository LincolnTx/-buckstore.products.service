using System;

namespace buckstore.products.service.application.IntegrationEvents
{
    public class StockConfirmationIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }
        public bool Success { get; set; }

        public StockConfirmationIntegrationEvent(Guid orderId, bool success) : base(DateTime.Now)
        {
            OrderId = orderId;
            Success = success;
        }
    }
}
