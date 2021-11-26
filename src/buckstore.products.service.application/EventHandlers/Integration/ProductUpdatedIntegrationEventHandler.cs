using System.Threading;
using System.Threading.Tasks;
using buckstore.products.service.application.IntegrationEvents;
using buckstore.products.service.domain.Aggregates.ProductAggregate;
using buckstore.products.service.domain.Exceptions;
using buckstore.products.service.domain.SeedWork;
using MediatR;

namespace buckstore.products.service.application.EventHandlers.Integration
{
    public class ProductUpdatedIntegrationEventHandler : EventHandler<ProductUpdatedIntegrationEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMediator _bus;

        public ProductUpdatedIntegrationEventHandler(IProductRepository productRepository, IUnitOfWork uow, IMediator bus)
        {
            _productRepository = productRepository;
            _uow = uow;
            _bus = bus;
        }

        public override async Task Handle(ProductUpdatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var productFound = await _productRepository.FindById(notification.Id);

            if (productFound == null)
            {
                await _bus.Publish(new ExceptionNotification("010",
                        "Produto para ser atualizado, não foi encontrado"),
                    cancellationToken);

                return;
            }

            productFound.UpdateProduct(notification.Name, notification.Description, notification.Price,
                notification.Quantity, notification.CategoryId);

            if (!await _uow.Commit())
            {
                await _bus.Publish(new ExceptionNotification("010",
                        "Erro ao salvar atualização no banco"),
                    cancellationToken);
            }
        }
    }
}
