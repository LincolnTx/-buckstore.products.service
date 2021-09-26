using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using buckstore.products.service.domain.SeedWork;

namespace buckstore.products.service.domain.Aggregates.ProductAggregate
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> FindById(Guid id);
        void Delete(Product product);
        IEnumerable<ProductsImagesCollection> GetProductImagesFromMongo(IEnumerable<string> imagesId);
    }
}
