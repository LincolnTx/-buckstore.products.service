using buckstore.products.service.application.Commands;
using FluentValidation;

namespace buckstore.products.service.application.Validations
{
    public class RemoveFavoriteCommandValidations : AbstractValidator<RemoveFavoriteCommand>
    {
        public RemoveFavoriteCommandValidations()
        {
            ValidateUserId();
            ValidateProductId();
        }

        private void ValidateUserId()
        {
            RuleFor(rate => rate.UserId)
                .NotEmpty()
                .WithMessage("O id do usuário deve ser informado")
                .WithMessage("004")
                .Must(candidate => CommonValidations.GuidValidator(candidate.ToString()))
                .WithMessage("O campo deve ser o uuid válido")
                .WithErrorCode("005");
        }

        private void ValidateProductId()
        {
            RuleFor(rate => rate.ProductId)
                .NotEmpty()
                .WithMessage("O id do produto deve ser informado")
                .WithMessage("006")
                .Must(candidate => CommonValidations.GuidValidator(candidate.ToString()))
                .WithMessage("O campo deve ser o uuid válido")
                .WithErrorCode("007");
        }
    }
}
