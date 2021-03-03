using System;

namespace buckstore.products.service.application.Queries.ResponseDTOs
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public int stock_quantity { get; set; }
        public int categoryId { get; set; }
        public string category { get; set; }
    }
}