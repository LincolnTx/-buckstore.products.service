using System;

namespace buckstore.products.service.application.Queries.ResponseDTOs
{
    public class ProductsImageDto
    {
        public string ProductId { get; set; }
        public string Image { get; set; }

        public ProductsImageDto(string productId, byte[] image, string contentType)
        {
            ProductId = productId;
            Image = BuildImage(image, contentType);
        }

        private string BuildImage(byte[] image, string type)
        {
            var base64 = Convert.ToBase64String(image, 0, image.Length);
            return $"data:{type};base64,{base64}";
        }
    }
}
