using System;
using System.Linq;
using System.Threading.Tasks;
using buckstore.products.service.domain.Aggregates.UserFavoriteAggregate;
using buckstore.products.service.infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace buckstore.products.service.infrastructure.Data.Repositories.UserFavoritesRepository
{
    public class UserFavoritesRepository : Repository<ProductFavorites>, IUserFavoritesRepository
    {
        public UserFavoritesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<bool> DeleteFavorite(Guid userId, Guid productId)
        {
            var itemDelete = await _dbSet
                .Where(item => item.ProductId == productId && item.UserId == userId)
                .FirstOrDefaultAsync();

            if (itemDelete == null)
                return false;

            _dbSet.Remove(itemDelete);
            return true;

        }
    }
}
