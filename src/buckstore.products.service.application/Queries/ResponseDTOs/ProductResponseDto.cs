using System;
using System.Collections.Generic;
using buckstore.products.service.application.Queries.ViewModels;

namespace buckstore.products.service.application.Queries.ResponseDTOs
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public List<ProductRateDto> ProductEvaluations { get; set; }

        public ProductResponseDto(FindProductWithRateVW product)
        {
            Id = product.Id;
            Name = product.name;
            Description = product.description;
            StockQuantity = product.stock_quantity;
            CategoryId = product.categoryId;
            Category = product.category;
            ProductEvaluations = new List<ProductRateDto>();
        }

        public void MergeRate(double rateValue, string comment, string username, string surname)
        {
            ProductEvaluations.Add(new ProductRateDto(rateValue, comment, username, surname));
        }
    }

    public class ProductRateDto
    {
        public double RateValue { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }
        public string Surname { get; set; }

        public ProductRateDto(double rateValue, string comment, string userName, string surname)
        {
            RateValue = rateValue;
            Comment = comment;
            UserName = userName;
            Surname = surname;
        }
    }
}