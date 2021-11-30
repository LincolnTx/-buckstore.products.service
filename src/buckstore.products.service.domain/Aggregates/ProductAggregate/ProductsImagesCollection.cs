using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace buckstore.products.service.domain.Aggregates.ProductAggregate
{
    public class ProductsImagesCollection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ImageId { get; set; }

        public byte[]  Image { get; set; }
        public string ContentType { get; set; }
    }
}
