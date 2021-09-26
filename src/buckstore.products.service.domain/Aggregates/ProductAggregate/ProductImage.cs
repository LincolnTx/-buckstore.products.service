using System;

namespace buckstore.products.service.domain.Aggregates.ProductAggregate
{
    public class ProductImage
    {
        public Guid Id { get; private set; }
        public byte[] Image { get; private set; }

        public ProductImage(Guid id, byte[] image)
        {
            Id = id;
            Image = image;
        }

        public ProductImage() { }

    }
}
