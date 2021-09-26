using System;
using System.Collections.Generic;
using System.Linq;
using buckstore.products.service.domain.SeedWork;

namespace buckstore.products.service.domain.Aggregates.ProductAggregate
{
    public class Product : Entity, IAggregateRoot
    {
        private string _name;
        public string Name => _name;
        private string _description;
        public string Description => _description;
        private decimal _price;
        public decimal Price => _price;
        private int _stockQuantity;
        public int Stock => _stockQuantity;
        private int _categoryId;
        public ProductCategory Category { get; private set; }
        public ICollection<ProductRate> RateList { get; private set; }
        public ICollection<ProductImage> Images { get; private set; }

        protected Product() { }

        public Product(string name, string description, decimal price, int stock, int categoryId)
        {
            _name = name;
            _description = description;
            _price = price;
            _stockQuantity = stock;
            _categoryId = categoryId;
            Category = ProductCategory.FromId(_categoryId);
            Images = new List<ProductImage>();
        }

        public void HandleProductImages(IEnumerable<byte[]> images)
        {
            foreach (var currentImage in images)
            {
                var productImage = new ProductImage(currentImage);
                Images.Add(productImage);
            }
        }

        public void AddStock(int quantity)
        {
            _stockQuantity += quantity;
        }

        public void DeductStock(int quantity)
        {
            _stockQuantity -= quantity;
        }

        public bool CheckAvailability()
        {
            return _stockQuantity > 0;
        }

        public void ChangeProductPrice(decimal newPrice)
        {
            _price = newPrice;
        }

        public void UpdateProduct(string name, string description, decimal price, int stock, int categoryId)
        {
            _name = name;
            _description = description;
            _price = price;
            _stockQuantity = stock;
            _categoryId = categoryId;
        }

        public void AddEvaluationToProduct(ProductRate rate)
        {
            RateList.Add(rate);
        }

        public void RemoveProductEvaluation(Guid evaluationId)
        {
            var removeItem = RateList.ToList().FirstOrDefault(rate => rate.Id == evaluationId);
            if (removeItem != null)
            {
                RateList.Remove(removeItem);
                return;
            }

            throw new NullReferenceException("Esse usuário não tem uma avaliação para esse produto");
        }

        public ProductRate FindEvaluateByUserId(Guid userId)
        {
            return RateList.ToList().FirstOrDefault(rate => rate.UserId == userId);
        }
    }
}
