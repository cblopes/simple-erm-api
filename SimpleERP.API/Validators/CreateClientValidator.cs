using FluentValidation;
using SimpleERP.API.Models;

namespace SimpleERP.API.Validators
{
    public class CreateClientValidator : AbstractValidator<CreateClientModel>
    {
        public CreateClientValidator() 
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                    .WithMessage("O nome não pode estar vazio ou nulo.")
                .MaximumLength(100)
                    .WithMessage("O nome não pode ter mais do que 100 caracteres.")
                .MinimumLength(3)
                    .WithMessage("O nome deve ter pelo menos 3 caracteres.");

            RuleFor(m => m.CpfCnpj)
                .NotEmpty()
                    .WithMessage("O CPF/CNPJ não pode estar vazio ou nulo.")
                .MaximumLength(14)
                    .WithMessage("O CPF/CNPJ não pode ter mais do que 14 digitos.")
                .MinimumLength(11)
                    .WithMessage("O CPF/CNPJ deve ter pelo menos 11 digitos.");
        }
    }
}
