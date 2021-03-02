using System;
using System.Threading.Tasks;
using buckstore.products.service.domain.Aggregates.ProductAggregate;
using buckstore.products.service.infrastructure.Data.Context;

namespace buckstore.products.service.infrastructure.Data.Repositories.ProductRepository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
           
        }

        public async Task<Product> FindById(Guid id)
        {
            return  await _dbSet.FindAsync(id);
        }

        public void Delete(Product product)
        {
            _dbSet.Remove(product);
        }
    }
}