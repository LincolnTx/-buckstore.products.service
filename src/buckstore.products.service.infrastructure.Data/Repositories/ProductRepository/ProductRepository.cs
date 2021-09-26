using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using buckstore.products.service.domain.Aggregates.ProductAggregate;
using buckstore.products.service.infrastructure.Data.Context;
using MongoDB.Driver;

namespace buckstore.products.service.infrastructure.Data.Repositories.ProductRepository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext applicationDbContext, MongoDbContext mongoDbContext) : base(applicationDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public async Task<Product> FindById(Guid id)
        {
            return  await _dbSet.FindAsync(id);
        }

        public void Delete(Product product)
        {
            _dbSet.Remove(product);
        }

        public IEnumerable<ProductsImagesCollection> GetProductImagesFromMongo(IEnumerable<string> imagesId)
        {
            var images = new List<ProductsImagesCollection>();
            try
            {
                var collection = _mongoDbContext.GetCollection<ProductsImagesCollection>(
                    Environment.GetEnvironmentVariable("MongoConfiguration__CollectionName"));

                foreach (var id in imagesId)
                {
                    var image = collection.Find(img => img.ImageId == id).First();

                    images.Add(image);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return images;
        }
    }
}
