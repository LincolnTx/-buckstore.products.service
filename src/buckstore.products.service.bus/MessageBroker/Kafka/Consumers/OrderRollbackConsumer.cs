using MediatR;
using Microsoft.Extensions.Logging;
using buckstore.products.service.application.IntegrationEvents;

namespace buckstore.products.service.bus.MessageBroker.Kafka.Consumers
{
    public class OrderRollbackConsumer : KafkaConsumer<OrderRollbackIntegrationEvent>
    {
        public OrderRollbackConsumer(IMediator bus, ILogger<OrderRollbackConsumer> logger) : base(bus, logger)
        {
        }
    }
}
