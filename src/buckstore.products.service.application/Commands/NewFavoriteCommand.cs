using System;
using MediatR;
using buckstore.products.service.application.Validations;

namespace buckstore.products.service.application.Commands
{
    public class NewFavoriteCommand : Command, IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }

        public NewFavoriteCommand(Guid userId, Guid productId)
        {
            UserId = userId;
            ProductId = productId;
        }

        public override bool IsValid()
        {
            ValidationResult = new NewFavoriteCommandValidations().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
