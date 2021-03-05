using System.Threading;
using System.Threading.Tasks;
using buckstore.products.service.application.Commands;
using buckstore.products.service.domain.Aggregates.ProductAggregate;
using buckstore.products.service.domain.Exceptions;
using buckstore.products.service.domain.SeedWork;
using MediatR;

namespace buckstore.products.service.application.CommandHandlers
{
    public class DeleteProductRateCommandHandler : CommandHandler, IRequestHandler<DeleteProductRateCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        
        public DeleteProductRateCommandHandler(IUnitOfWork uow,
            IMediator bus,
            INotificationHandler<ExceptionNotification> notifications,
            IProductRepository productRepository) : base(uow,
            bus,
            notifications)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProductRateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var product = await _productRepository.FindById(request.ProductId);
            product.RemoveProductEvaluation(request.RateId);

            if (await Commit())
                return true;

            await _bus.Publish(new ExceptionNotification("002", "Erro ao remover avaliação do produto"));
            return false;
        }
    }
}