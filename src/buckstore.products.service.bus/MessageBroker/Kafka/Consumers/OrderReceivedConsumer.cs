using MediatR;
using Microsoft.Extensions.Logging;
using buckstore.products.service.application.IntegrationEvents;

namespace buckstore.products.service.bus.MessageBroker.Kafka.Consumers
{
    public class OrderReceivedConsumer : KafkaConsumer<OrderReceivedIntegrationEvent>
    {
        public OrderReceivedConsumer(IMediator bus, ILogger<OrderReceivedConsumer> logger) : base(bus, logger)
        {
        }
    }
}