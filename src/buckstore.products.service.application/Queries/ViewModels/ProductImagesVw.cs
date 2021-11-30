using System;

namespace buckstore.products.service.application.Queries.ViewModels
{
    public class ProductImagesVw
    {
        public byte[] Image { get; set; }
        public string ContentType { get; set; }
        public Guid product_id { get; set; }

    }
}
