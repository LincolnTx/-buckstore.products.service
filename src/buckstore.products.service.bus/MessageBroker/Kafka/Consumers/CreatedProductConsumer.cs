using MediatR;
using Microsoft.Extensions.Logging;
using buckstore.products.service.application.IntegrationEvents;

namespace buckstore.products.service.bus.MessageBroker.Kafka.Consumers
{
    public class CreatedProductConsumer : KafkaConsumer<ProductCreatedIntegrationEvent>
    {
        public CreatedProductConsumer(IMediator bus, ILogger<CreatedProductConsumer> logger) : base(bus, logger)
        {
        }
    }
}