using buckstore.products.service.infrastructure.Data.Context;
using buckstore.products.service.domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace buckstore.products.service.infrastructure.Data.Repositories
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
	{
		protected readonly ApplicationDbContext _applicationDbContext;
		protected readonly DbSet<TEntity> _dbSet;
        protected MongoDbContext _mongoDbContext;

		public Repository( ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
			_dbSet = applicationDbContext.Set<TEntity>();
		}

		public void Add(TEntity obj)
		{
			_applicationDbContext.Add(obj);
		}
	}
}
