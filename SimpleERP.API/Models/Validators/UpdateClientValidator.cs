using FluentValidation;
using SimpleERP.API.Models;

namespace SimpleERP.API.Models.Validators
{
    public class UpdateClientValidator : AbstractValidator<UpdateClientModel>
    {
        public UpdateClientValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                    .WithMessage("O nome não pode estar vazio ou nulo.")
                .MinimumLength(3)
                    .WithMessage("O nome deve ter pelo menos 3 caracteres.")
                .MaximumLength(100)
                    .WithMessage("O nome não pode ter mais do que 100 caracteres.");

        }
    }
}
