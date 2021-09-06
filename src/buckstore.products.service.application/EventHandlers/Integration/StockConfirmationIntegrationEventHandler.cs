using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using buckstore.products.service.application.IntegrationEvents;
using buckstore.products.service.application.Adapters.MessageBroker;

namespace buckstore.products.service.application.EventHandlers.Integration
{
    public class StockConfirmationIntegrationEventHandler : EventHandler<StockConfirmationIntegrationEvent>
    {
        private readonly IMessageProducer<StockConfirmationIntegrationEvent> _producer;
        private readonly ILogger<StockConfirmationIntegrationEventHandler> _logger;

        public StockConfirmationIntegrationEventHandler(IMessageProducer<StockConfirmationIntegrationEvent> producer, ILogger<StockConfirmationIntegrationEventHandler> logger)
        {
            _producer = producer;
            _logger = logger;
        }

        public override async Task Handle(StockConfirmationIntegrationEvent notification, CancellationToken cancellationToken)
        {
            await _producer.Produce(notification);
            _logger.LogInformation($"Confirmação de estoque Bem sucedida enviada para a ordem {notification.OrderId}");
        }
    }
}
