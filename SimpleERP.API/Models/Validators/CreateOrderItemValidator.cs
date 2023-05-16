using FluentValidation;

namespace SimpleERP.API.Models.Validators
{
    public class CreateOrderItemValidator : AbstractValidator<CreateOrderItemModel>
    {
        public CreateOrderItemValidator()
        {
            RuleFor(m => m.Code)
                .NotEmpty()
                    .WithMessage("O código do produto é obrigatório.")
                .MaximumLength(10)
                    .WithMessage("O código do produto deve conter no máximo 10 caracteres.");

            RuleFor(m => m.Quantity)
                .GreaterThan(0)
                    .WithMessage("Quantidade deve ser maior que 0 (zero).");
        }
    }
}
