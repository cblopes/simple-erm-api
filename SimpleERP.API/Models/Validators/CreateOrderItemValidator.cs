using FluentValidation;

namespace SimpleERP.API.Models.Validators
{
    public class CreateOrderItemValidator : AbstractValidator<CreateOrderItemModel>
    {
        public CreateOrderItemValidator()
        {
            RuleFor(m => m.ProductId)
                .NotEmpty()
                    .WithMessage("Identificação do produto não pode ser vazia ou nula");

            RuleFor(m => m.Quantity)
                .GreaterThan(0)
                    .WithMessage("Quantidade deve ser maior que 0 (zero).");
        }
    }
}
