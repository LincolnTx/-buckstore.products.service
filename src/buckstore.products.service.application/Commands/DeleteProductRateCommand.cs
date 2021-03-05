using System;
using buckstore.products.service.application.Validations;
using MediatR;

namespace buckstore.products.service.application.Commands
{
    public class DeleteProductRateCommand : Command, IRequest<bool>
    {
        public Guid RateId { get; set; }
        public Guid ProductId { get; set; }
        
        public override bool IsValid()
        {
            ValidationResult = new DeleteProductRateValidations().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}