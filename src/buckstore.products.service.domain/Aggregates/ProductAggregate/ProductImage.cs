using System;

namespace buckstore.products.service.domain.Aggregates.ProductAggregate
{
    public class ProductImage
    {
        public Guid Id { get; private set; }
        public byte[] Image { get; private set; }
        public string ContentType { get; private set; }

        public ProductImage(Guid id, byte[] image, string contentType)
        {
            Id = id;
            Image = image;
            ContentType = contentType;
        }

        public ProductImage(string contentType)
        {
            ContentType = contentType;
        }

    }
}
