using FluentValidation;

namespace SimpleERP.API.Models.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductModel>
    {
        public CreateProductValidator()
        {
            RuleFor(m => m.Code)
                .NotEmpty()
                    .WithMessage("O código não pode ser vazio ou nulo.")
                .MaximumLength(10)
                    .WithMessage("O código deve conter no máximo 10 caracteres.");

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
