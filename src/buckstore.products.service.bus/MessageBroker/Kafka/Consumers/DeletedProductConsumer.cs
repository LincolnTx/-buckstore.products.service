using MediatR;
using Microsoft.Extensions.Logging;
using buckstore.products.service.application.IntegrationEvents;

namespace buckstore.products.service.bus.MessageBroker.Kafka.Consumers
{
    public class DeletedProductConsumer : KafkaConsumer<ProductDeletedIntegrationEvent>
    {
        public DeletedProductConsumer(IMediator bus, ILogger<DeletedProductConsumer> logger) : base(bus, logger)
        {
        }
    }
}