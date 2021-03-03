using System;
using buckstore.products.service.application.Validations;
using MediatR;

namespace buckstore.products.service.application.Commands
{
    public class AddProductRateCommand : Command, IRequest<bool>
    {
        public Guid ProductCode { get; set; }
        public double RatePoints { get; set; }
        public string Comment { get; set; }
        public Guid UserId { get; set; }
        
        public override bool IsValid()
        {
            ValidationResult = new AddProductRateValidations().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}