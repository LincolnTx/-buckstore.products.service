using System;
using MediatR;

namespace buckstore.products.service.application.IntegrationEvents
{
    public class IntegrationEvent : INotification
    {
        public DateTime Timestamp { get; set; }

        public IntegrationEvent(DateTime timestamp)
        {
            Timestamp = timestamp;
        }
    }
}