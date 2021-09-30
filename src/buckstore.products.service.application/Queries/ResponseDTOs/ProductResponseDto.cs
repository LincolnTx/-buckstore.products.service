using System;
using System.Collections;
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
        public decimal AverageRate { get; set; }
        public List<ProductRateDto> ProductEvaluations { get; set; }
        public List<string> Images { get; set; }

        public ProductResponseDto(FindProductWithRateVW product)
        {
            Id = product.Id;
            Name = product.name;
            Description = product.description;
            StockQuantity = product.stock_quantity;
            CategoryId = product.categoryId;
            Category = product.category;
            ProductEvaluations = new List<ProductRateDto>();
            Images = new List<string>();
            Price = product.price;
        }

        public void MergeRate(Guid rateId, double rateValue, string comment, string username)
        {
            ProductEvaluations.Add(new ProductRateDto(rateId, rateValue, comment, username));
        }

        public void SetImagesUrl(IEnumerable<string> images)
        {
            Images.AddRange(images);
        }
    }

    public class ProductRateDto
    {
        public Guid RateId { get; set; }
        public double RateValue { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }

        public ProductRateDto(Guid rateId, double rateValue, string comment, string userName)
        {
            RateId = rateId;
            RateValue = rateValue;
            Comment = comment;
            UserName = userName;
        }
    }
}
