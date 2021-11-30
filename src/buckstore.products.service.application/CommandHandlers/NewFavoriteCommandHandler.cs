using MediatR;
using System.Threading;
using System.Threading.Tasks;
using buckstore.products.service.domain.SeedWork;
using buckstore.products.service.domain.Exceptions;
using buckstore.products.service.application.Commands;
using buckstore.products.service.domain.Aggregates.UserFavoriteAggregate;

namespace buckstore.products.service.application.CommandHandlers
{
    public class NewFavoriteCommandHandler : CommandHandler, IRequestHandler<NewFavoriteCommand, bool>
    {
        private readonly IUserFavoritesRepository _favoritesRepository;
        public NewFavoriteCommandHandler(IUnitOfWork uow, IMediator bus, INotificationHandler<ExceptionNotification> notifications, IUserFavoritesRepository favoritesRepository)
            : base(uow, bus, notifications)
        {
            _favoritesRepository = favoritesRepository;
        }

        public async Task<bool> Handle(NewFavoriteCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var favorite = new ProductFavorites(request.UserId, request.ProductId);

            _favoritesRepository.Add(favorite);

            if (await Commit())
                return true;

            await _bus.Publish(
                new ExceptionNotification("012",
                    "Erro ao favoritar este producto, confira se as informações estão corretas"),
                cancellationToken);

            return false;
        }
    }
}
