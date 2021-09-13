using MediatR;
using System.Threading;
using System.Threading.Tasks;
using buckstore.products.service.domain.SeedWork;
using buckstore.products.service.domain.Exceptions;
using buckstore.products.service.application.IntegrationEvents;
using buckstore.products.service.domain.Aggregates.ProductAggregate;

namespace buckstore.products.service.application.EventHandlers.Integration
{
    public class OrderRollbackIntegrationEventHandler : EventHandler<OrderRollbackIntegrationEvent>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediator _bus;
        private readonly IProductRepository _productRepository;

        public OrderRollbackIntegrationEventHandler(IUnitOfWork uow, IMediator bus, IProductRepository productRepository)
        {
            _uow = uow;
            _bus = bus;
            _productRepository = productRepository;
        }

        public override async Task Handle(OrderRollbackIntegrationEvent notification, CancellationToken cancellationToken)
        {
            foreach (var item in notification.Products)
            {
                var product = await _productRepository.FindById(item.ProductId);

                product.AddStock(item.Quantity);
            }

            if (!await _uow.Commit())
            {
                await _bus.Publish(new ExceptionNotification("012",
                    "Erro ao realizar rollback em um ou mais produtos")
                    , cancellationToken);
                return;
            }

            await _bus.Publish(new ProductRollbackIntegrationEvent(notification.Products),
                cancellationToken);
        }
    }
}
