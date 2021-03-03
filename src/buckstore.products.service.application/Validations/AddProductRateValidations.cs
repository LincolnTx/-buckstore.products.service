using buckstore.products.service.application.Commands;
using FluentValidation;

namespace buckstore.products.service.application.Validations
{
    public class AddProductRateValidations : AbstractValidator<AddProductRateCommand>
    {
        public AddProductRateValidations()
        {
            // adicionar validações
        }
    }
}