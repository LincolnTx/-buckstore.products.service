using FluentValidation;
using buckstore.products.service.application.Commands;

namespace buckstore.products.service.application.Validations
{
    public class DeleteProductRateValidations : AbstractValidator<DeleteProductRateCommand>
    {
        public DeleteProductRateValidations()
        {
            ValidateRateId();
            ValidateProductId();
        }

        private void ValidateRateId()
        {
            RuleFor(rate => rate.RateId)
                .NotEmpty()
                .WithMessage("O id da avaliação deve ser informado")
                .WithMessage("001")
                .Must(candidate => CommonValidations.GuidValidator(candidate.ToString()))
                .WithMessage("O campo deve ser o uuid válido")
                .WithErrorCode("002");
        }

        private void ValidateProductId()
        {
            RuleFor(rate => rate.ProductId)
                .NotEmpty()
                .WithMessage("O id do produto deve ser informado")
                .WithMessage("003")
                .Must(candidate => CommonValidations.GuidValidator(candidate.ToString()))
                .WithMessage("O campo deve ser o uuid válido")
                .WithErrorCode("004");
        }
    }
}