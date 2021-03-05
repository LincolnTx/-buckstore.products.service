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
        private double _price;
        public double Price => _price;
        private int _stockQuantity;
        public int Stock => _stockQuantity;
        private int _categoryId;
        public ProductCategory Category { get; private set; }
        public ICollection<ProductRate> RateList { get; private set; }
        
        protected Product() { }

        public Product(string name, string description, double price, int stock, int categoryId)
        {
            _name = name;
            _description = description;
            _price = price;
            _stockQuantity = stock;
            _categoryId = categoryId;
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

        public void ChangeProductPrice(double newPrice)
        {
            _price = newPrice;
        }

        public void UpdateProduct(string name, string description, double price, int stock, int categoryId)
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
    }
}