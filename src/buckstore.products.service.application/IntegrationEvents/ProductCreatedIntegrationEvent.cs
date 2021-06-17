using System;

namespace buckstore.products.service.application.IntegrationEvents
{
    public class ProductCreatedIntegrationEvent : IntegrationEvent
    {

        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public ProductCreatedIntegrationEvent(Guid id, string productName, int quantity, decimal price, DateTime timestamp) 
            : base(timestamp)
        {
            Id = id;
            ProductName = productName;
            Quantity = quantity;
            Price = price;
        }
    }
}