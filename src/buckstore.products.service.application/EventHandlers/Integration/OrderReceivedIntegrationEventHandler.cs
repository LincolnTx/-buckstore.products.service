using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using buckstore.products.service.domain.SeedWork;
using buckstore.products.service.application.IntegrationEvents;
using buckstore.products.service.domain.Aggregates.ProductAggregate;

namespace buckstore.products.service.application.EventHandlers.Integration
{
    public class OrderReceivedIntegrationEventHandler : EventHandler<OrderReceivedIntegrationEvent>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediator _bus;
        private readonly IProductRepository _productRepository;

        public OrderReceivedIntegrationEventHandler(IProductRepository productRepository, IUnitOfWork uow, IMediator bus)
        {
            _productRepository = productRepository;
            _uow = uow;
            _bus = bus;
        }

        public override async Task Handle(OrderReceivedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var errorList = new List<string>();

            foreach (var product in notification.Products)
            {
                var response = await _productRepository.FindById(product.ProductId);

                if (product.Quantity > response.Stock)
                {
                    errorList.Add(product.ProductName);
                    continue;
                }

                response.DeductStock(product.Quantity);
            }

            if (errorList.Count > 0)
            {
                await _bus.Publish(new StockConfirmationFailIntegrationEvent(notification.OrderId,
                    false,
                    $"Um ou mais produtos não possuem estoque suficiente: {string.Join(" ,", errorList)}"
                ), cancellationToken);

                return;
            }

            if (!await _uow.Commit())
            {
                await _bus.Publish(new StockConfirmationFailIntegrationEvent(notification.OrderId,
                    false,
                    $"Erro durante processamento da ordem"
                ), cancellationToken);

                return;
            }

            await _bus.Publish(new StockConfirmationIntegrationEvent(notification.OrderId, true), cancellationToken);
        }
    }
}
