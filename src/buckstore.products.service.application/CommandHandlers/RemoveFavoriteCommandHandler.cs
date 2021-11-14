using MediatR;
using System.Threading;
using System.Threading.Tasks;
using buckstore.products.service.domain.SeedWork;
using buckstore.products.service.domain.Exceptions;
using buckstore.products.service.application.Commands;
using buckstore.products.service.domain.Aggregates.UserFavoriteAggregate;

namespace buckstore.products.service.application.CommandHandlers
{
    public class RemoveFavoriteCommandHandler : CommandHandler, IRequestHandler<RemoveFavoriteCommand, Unit>
    {
        private readonly IUserFavoritesRepository _favoritesRepository;
        public RemoveFavoriteCommandHandler(IUnitOfWork uow,
            IMediator bus,
            INotificationHandler<ExceptionNotification> notifications,
            IUserFavoritesRepository favoritesRepository)
            : base(uow, bus, notifications)
        {
            _favoritesRepository = favoritesRepository;
        }

        public async Task<Unit> Handle(RemoveFavoriteCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);

                return Unit.Value;
            }

            var couldDelete = await _favoritesRepository.DeleteFavorite(request.UserId, request.ProductId);

            if (!await Commit() || !couldDelete)
            {
                await _bus.Publish(
                    new ExceptionNotification("015", "Erro ao remover um favorito, tente novamente mais tarde"),
                    cancellationToken);
            }

            return Unit.Value;
        }
    }
}
