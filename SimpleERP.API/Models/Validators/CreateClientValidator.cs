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
            if (string.IsNullOrEmpty(cnpj))
            {
                return false;
            }

            cnpj = cnpj.Trim().Replace(".", "").Replace("/", "").Replace("-", "");

            if (cnpj.Length != 14)
            {
                return false;
            }

            int[] digits = new int[14];

            for (int i = 0; i < 14; i++)
            {
                if (!int.TryParse(cnpj[i].ToString(), out digits[i]))
                {
                    return false;
                }
            }

            int[] multipliers1 = new int[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multipliers2 = new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int sum1 = 0;
            int sum2 = 0;

            for (int i = 0; i < 12; i++)
            {
                sum1 += digits[i] * multipliers1[i];
                sum2 += digits[i] * multipliers2[i];
            }

            sum2 += digits[12] * multipliers2[12];

            int mod1 = sum1 % 11;
            int mod2 = sum2 % 11;

            if (mod1 < 2)
            {
                mod1 = 0;
            }
            else
            {
                mod1 = 11 - mod1;
            }

            if (mod2 < 2)
            {
                mod2 = 0;
            }
            else
            {
                mod2 = 11 - mod2;
            }

            return (digits[12] == mod1 && digits[13] == mod2);
        }

        private bool IsOnlyNumbers(string input)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            return regex.IsMatch(input);
        }
    }
}
