using System;
using System.Threading.Tasks;
using buckstore.products.service.domain.SeedWork;

namespace buckstore.products.service.domain.Aggregates.UserFavoriteAggregate
{
    public interface IUserFavoritesRepository : IRepository<ProductFavorites>
    {
        Task<bool> DeleteFavorite(Guid userId, Guid productId);
    }
}
