using System.Collections.Generic;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using buckstore.products.service.domain.SeedWork;
using buckstore.products.service.domain.Exceptions;
using buckstore.products.service.application.IntegrationEvents;
using buckstore.products.service.domain.Aggregates.ProductAggregate;

namespace buckstore.products.service.application.EventHandlers.Integration
{
    public class ProductCreatedIntegrationEventHandler : INotificationHandler<ProductCreatedIntegrationEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMediator _bus;
        private readonly IMapper _mapper;

        public ProductCreatedIntegrationEventHandler(IProductRepository productRepository, IUnitOfWork uow, IMediator bus, IMapper mapper)
        {
            _productRepository = productRepository;
            _uow = uow;
            _bus = bus;
            _mapper = mapper;
        }

        public async Task Handle(ProductCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var imagesCollections = _productRepository.GetProductImagesFromMongo(notification.ImagesId);

            var productImages = _mapper.Map<List<ProductImage>>(imagesCollections);

            var product = new Product(notification.Id, notification.Name, notification.Description, notification.Price,
                notification.Quantity, notification.CategoryId, productImages);


            _productRepository.Add(product);

            if (!await _uow.Commit())
            {
                await _bus.Publish(new ExceptionNotification("001",
                    "Erro ao cadastrar produto, tente novamente mais tarde ou entre em contato com o suporte"));
            }
        }
    }
}
