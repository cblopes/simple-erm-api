using FluentValidation;

namespace SimpleERP.API.Models.Validators
{
    public class AlterProductValidator : AbstractValidator<AlterProductModel>
    {
        public AlterProductValidator()
        {
            RuleFor(m => m.Description)
                .NotEmpty()
                    .WithMessage("A descrição não pode ser vazia ou nula.")
                .MinimumLength(3)
                    .WithMessage("A descrição deve conter no mínimo 3 caracteres")
                .MaximumLength(100)
                    .WithMessage("A descrição deve conter no máximo 100 caracteres");

            RuleFor(m => m.QuantityInStock)
                .GreaterThanOrEqualTo(0)
                    .WithMessage("A quantidade em estoque deve ser maior ou igual a zero.");

            RuleFor(m => m.Price)
                .NotEmpty()
                    .WithMessage("O preço não pode ser vazio ou nulo.")
                .GreaterThan(0)
                    .WithMessage("O preço deve ser maior que zero.");
        }
    }
}
