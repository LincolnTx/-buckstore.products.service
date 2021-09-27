using System;

namespace buckstore.products.service.domain.Aggregates.ProductAggregate
{
    public class ProductRate
    {
        public Guid Id { get; set; }
        public double RateValue { get; private set; }
        public string Comment { get; private set; }
        public Guid UserId { get; private set; }
        public string UserName { get; set; }

        public ProductRate(double rateValue, string comment, Guid userId, string userName)
        {
            Id = new Guid();
            RateValue = rateValue;
            Comment = comment;
            UserId = userId;
            UserName = userName;
        }
    }
}
