using FluentValidation;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace SimpleERP.API.Models.Validators
{
    public class CreateClientValidator : AbstractValidator<CreateClientModel>
    {
        public CreateClientValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                    .WithMessage("O nome não pode estar vazio ou nulo.")
                .MaximumLength(100)
                    .WithMessage("O nome não pode conter mais do que 100 caracteres.")
                .MinimumLength(3)
                    .WithMessage("O nome deve conter pelo menos 3 caracteres.");

            RuleFor(m => m.CpfCnpj)
                .NotEmpty()
                    .WithMessage("O CPF/CNPJ não pode estar vazio ou nulo.")
                .Must(IsDocumentValid)
                    .WithMessage("CPF/CNPJ inválido.");
        }

        private bool IsDocumentValid(string document)
        {
            document = document.Replace(".", "").Replace("-", "").Replace("/", "");            

            if (document.Length == 11)
            {
                return IsCpfValid(document);
            }
            
            if (document.Length == 14)
            {
                return IsCnpjValid(document);
            }

            return false;
        }

        private bool IsCpfValid(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (!IsOnlyNumbers(cpf))
            {
                return false;
            }

            if (cpf.Length != 11 || !long.TryParse(cpf, out long _))
            {
                return false;
            }

            string firstNineDigits = cpf[..9];

            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(firstNineDigits[i].ToString()) * (10 - i);
            }

            int mod = sum % 11;

            if (mod == 1 || mod == 0)
            {
                if (cpf[9] != '0')
                {
                    return false;
                }
            }
            else if (cpf[9] != (char)(11 - mod + '0'))
            {
                return false;
            }

            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(cpf[i].ToString()) * (11 - i);
            }

            mod = sum % 11;

            if (mod == 1 || mod == 0)
            {
                if (cpf[10] != 0)
                {
                    return false;
                }
            }
            else if (cpf[10] != (char)(11 - mod + '0'))
            {
                return false;
            }

            return true;
        }

        private bool IsCnpjValid(string cnpj)
        {
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (!IsOnlyNumbers(cnpj))
            {
                return false;
            }

            return true;
        }

        private bool IsOnlyNumbers(string input)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            return regex.IsMatch(input);
        }
    }
}
