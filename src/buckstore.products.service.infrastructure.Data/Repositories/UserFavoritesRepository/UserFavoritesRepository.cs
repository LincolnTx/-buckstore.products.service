using buckstore.products.service.domain.Aggregates.UserFavoriteAggregate;
using buckstore.products.service.infrastructure.Data.Context;

namespace buckstore.products.service.infrastructure.Data.Repositories.UserFavoritesRepository
{
    public class UserFavoritesRepository : Repository<ProductFavorites>, IUserFavoritesRepository
    {
        public UserFavoritesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
