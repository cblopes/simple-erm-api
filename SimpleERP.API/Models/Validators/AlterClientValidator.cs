using FluentValidation;
using SimpleERP.API.Models;

namespace SimpleERP.API.Models.Validators
{
    public class AlterClientValidator : AbstractValidator<AlterClientModel>
    {
        public AlterClientValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                    .WithMessage("O nome não pode estar vazio ou nulo.")
                .MinimumLength(3)
                    .WithMessage("O nome deve conter pelo menos 3 caracteres.")
                .MaximumLength(100)
                    .WithMessage("O nome não pode conter mais do que 100 caracteres.");
        }
    }
}
