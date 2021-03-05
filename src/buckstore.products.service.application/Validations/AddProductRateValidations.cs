using FluentValidation;
using buckstore.products.service.application.Commands;

namespace buckstore.products.service.application.Validations
{
    public class AddProductRateValidations : AbstractValidator<AddProductRateCommand>
    {
        
        public AddProductRateValidations()
        {
           ValidateProductCode();
           ValidateRatePoints();
           ValidateComment();
           ValidateUserId();
        }

        private void ValidateProductCode()
        {
            RuleFor(rate => rate.ProductCode)
                .NotEmpty()
                .WithMessage("Código do produto deve ser informado para realizar essa alteração")
                .WithErrorCode("001")
                .Must(candidate => CommonValidations.GuidValidator(candidate.ToString()))
                .WithMessage("O código deve ser um uuid válido!")
                .WithErrorCode("002");
        }

        private void ValidateRatePoints()
        {
            RuleFor(rate => rate.RatePoints)
                .NotEmpty()
                .WithMessage("Para adicionar uma avaliação o puntuação é obrigatória")
                .WithErrorCode("003")
                .ExclusiveBetween(1, 5)
                .WithMessage("O valor da nota deve ser algo entre 1 e 5")
                .WithErrorCode("004");
        }

        private void ValidateComment()
        {
            RuleFor(rate => rate.Comment)
                .MaximumLength(300)
                .WithMessage("O seu comentário ultrapassou o limete de caracteres, o máximo permitido é 300")
                .WithErrorCode("005");
        }

        private void ValidateUserId()
        {
            RuleFor(rate => rate.UserId)
                .NotEmpty()
                .WithMessage("Por favor indique o id do usuário para realizar o cadastro")
                .WithErrorCode("006")
                .Must(candidate => CommonValidations.GuidValidator(candidate.ToString()));
        }

        
    }
}