using MediatR;
using Microsoft.Extensions.Logging;
using buckstore.products.service.application.IntegrationEvents;

namespace buckstore.products.service.bus.MessageBroker.Kafka.Consumers
{
    public class UpdatedProductConsumer : KafkaConsumer<ProductUpdatedIntegrationEvent>
    {
        public UpdatedProductConsumer(IMediator bus, ILogger<UpdatedProductConsumer> logger) : base(bus, logger)
        {
        }
    }
}