using System;

namespace buckstore.products.service.application.IntegrationEvents
{
    public class StockConfirmationFailIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }
        public bool Success { get; set; }
        public string RejectReason { get; set; }

        public StockConfirmationFailIntegrationEvent(Guid orderId, bool success, string rejectReason) : base(DateTime.Now)
        {
            OrderId = orderId;
            Success = success;
            RejectReason = rejectReason;
        }
    }
}
