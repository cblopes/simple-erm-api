using FluentValidation;

namespace SimpleERP.API.Models.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderModel>
    {
        public CreateOrderValidator()
        {
            RuleFor(m => m.ClientId)
                .NotEmpty()
                    .WithMessage("Identificação do produto não pode ser vazia ou nula");
        }
    }
}
