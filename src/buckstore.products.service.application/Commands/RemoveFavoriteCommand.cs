using System;
using MediatR;
using buckstore.products.service.application.Validations;

namespace buckstore.products.service.application.Commands
{
    public class RemoveFavoriteCommand : Command, IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }

        public RemoveFavoriteCommand(Guid userId, Guid productId)
        {
            UserId = userId;
            ProductId = productId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveFavoriteCommandValidations().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
