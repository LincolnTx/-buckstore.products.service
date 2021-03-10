using MediatR;
using System.Threading;
using System.Threading.Tasks;
using buckstore.products.service.application.Commands;
using buckstore.products.service.domain.Aggregates.ProductAggregate;
using buckstore.products.service.domain.Exceptions;
using buckstore.products.service.domain.SeedWork;

namespace buckstore.products.service.application.CommandHandlers
{
    public class AddProductRateCommandHandler : CommandHandler, IRequestHandler<AddProductRateCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        public AddProductRateCommandHandler(IUnitOfWork uow,
            IMediator bus,
            INotificationHandler<ExceptionNotification> notifications,
            IProductRepository productRepository) 
            : base(uow, bus, notifications)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(AddProductRateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var product = await _productRepository.FindById(request.ProductCode);

            if (product == null)
            {
                await _bus.Publish(new ExceptionNotification("001", "Produto informado não encontrato."),
                    CancellationToken.None);
                
                return false;
            }
            var evaluation = product.FindEvaluateByUserId(request.UserId);
            var userEvaluation = new ProductRate(request.RatePoints, request.Comment, request.UserId);

            if (evaluation != null)
            {
                product.RemoveProductEvaluation(evaluation.Id);
            }
            product.AddEvaluationToProduct(userEvaluation);

            if (await Commit())
                return true;
            
            await _bus.Publish(new ExceptionNotification("002",
                    "Algo de errado aconteceu ao adicionar sua avaliaçõa! Tente mais tarde."),
                CancellationToken.None);
            
            return false;
        }
    }
}