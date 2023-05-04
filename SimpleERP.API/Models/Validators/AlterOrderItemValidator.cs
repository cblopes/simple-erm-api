using FluentValidation;

namespace SimpleERP.API.Models.Validators
{
    public class AlterOrderItemValidator : AbstractValidator<AlterOrderItemModel>
    {
        public AlterOrderItemValidator()
        {
            RuleFor(m => m.Quantity)
                .GreaterThan(0)
                    .WithMessage("Quantidade deve ser maior que 0 (zero).");
        }
    }
}
