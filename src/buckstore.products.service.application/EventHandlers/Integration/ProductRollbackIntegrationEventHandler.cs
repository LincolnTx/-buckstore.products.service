using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using buckstore.products.service.application.IntegrationEvents;
using buckstore.products.service.application.Adapters.MessageBroker;

namespace buckstore.products.service.application.EventHandlers.Integration
{
    public class ProductRollbackIntegrationEventHandler : EventHandler<ProductRollbackIntegrationEvent>
    {
        private readonly IMessageProducer<ProductRollbackIntegrationEvent> _producer;
        private readonly ILogger<ProductRollbackIntegrationEventHandler> _logger;

        public ProductRollbackIntegrationEventHandler(IMessageProducer<ProductRollbackIntegrationEvent> producer, ILogger<ProductRollbackIntegrationEventHandler> logger)
        {
            _producer = producer;
            _logger = logger;
        }

        public override async Task Handle(ProductRollbackIntegrationEvent notification, CancellationToken cancellationToken)
        {
            await _producer.Produce(notification);
            _logger.LogInformation("Rollback de estoque de produtos devido a erro na ordem enviado para a manager");
        }
    }
}
