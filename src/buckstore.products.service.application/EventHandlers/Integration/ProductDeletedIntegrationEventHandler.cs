using MediatR;
using System.Threading;
using System.Threading.Tasks;
using buckstore.products.service.domain.SeedWork;
using buckstore.products.service.domain.Exceptions;
using buckstore.products.service.application.IntegrationEvents;
using buckstore.products.service.domain.Aggregates.ProductAggregate;

namespace buckstore.products.service.application.EventHandlers.Integration
{
    public class ProductDeletedIntegrationEventHandler : EventHandler<ProductDeletedIntegrationEvent>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediator _bus;
        private readonly IProductRepository _productRepository;

        public ProductDeletedIntegrationEventHandler(IUnitOfWork uow, IMediator bus, IProductRepository productRepository)
        {
            _uow = uow;
            _bus = bus;
            _productRepository = productRepository;
        }

        public async override Task Handle(ProductDeletedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var deleteProduct = await _productRepository.FindById(notification.Id);

            if (deleteProduct == null)
            {
                await _bus.Publish(new ExceptionNotification("011",
                        "Produto para ser deletado, não foi encontrado"),
                    cancellationToken);

                return;
            }

            _productRepository.Delete(deleteProduct);

            if (!await _uow.Commit())
            {
                await _bus.Publish(new ExceptionNotification("011",
                        $"Erro ao salvar atualização no banco, tentativa de deletar produto {notification.Id}"),
                    cancellationToken);
            }
        }
    }
}
