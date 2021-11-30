using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using buckstore.products.service.application.IntegrationEvents;
using buckstore.products.service.application.Adapters.MessageBroker;

namespace buckstore.products.service.application.EventHandlers.Integration
{
    public class StockConfirmationFailIntegrationEventHandler : EventHandler<StockConfirmationFailIntegrationEvent>
    {
        private readonly IMessageProducer<StockConfirmationFailIntegrationEvent> _producer;
        private readonly ILogger<StockConfirmationFailIntegrationEventHandler> _logger;

        public StockConfirmationFailIntegrationEventHandler(IMessageProducer<StockConfirmationFailIntegrationEvent> producer, ILogger<StockConfirmationFailIntegrationEventHandler> logger)
        {
            _producer = producer;
            _logger = logger;
        }

        public override async Task Handle(StockConfirmationFailIntegrationEvent notification, CancellationToken cancellationToken)
        {
            await _producer.Produce(notification);
            _logger.LogInformation($"Confirmação de estoque falha enviada para a ordem {notification.OrderId}");;
        }
    }
}
