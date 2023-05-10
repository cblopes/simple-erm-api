using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleERP.MVC.Extensions
{
    public class CpfValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string cpf = value.ToString();

                // Remove caracteres não numéricos
                cpf = new string(cpf.Where(char.IsDigit).ToArray());

                // Verifica se o CPF possui 11 dígitos
                if (cpf.Length == 11)
                {
                    // Verifica se todos os dígitos são iguais (CPF inválido)
                    if (!AllDigitsAreEqual(cpf))
                    {
                        // Calcula o primeiro dígito verificador
                        int digit1 = CalculateDigit(cpf.Substring(0, 9));

                        // Calcula o segundo dígito verificador
                        int digit2 = CalculateDigit(cpf.Substring(0, 9) + digit1);

                        // Verifica se os dígitos verificadores estão corretos
                        if (cpf.EndsWith(digit1.ToString() + digit2.ToString()))
                        {
                            return ValidationResult.Success;
                        }
                    }
                }
            }

            return new ValidationResult(ErrorMessage);
        }

        private int CalculateDigit(string partialCpf)
        {
            int sum = 0;
            int factor = 10;

            foreach (char c in partialCpf)
            {
                sum += int.Parse(c.ToString()) * factor;
                factor--;
            }

            int remainder = sum % 11;
            int digit = remainder < 2 ? 0 : 11 - remainder;

            return digit;
        }

        private bool AllDigitsAreEqual(string cpf)
        {
            char firstDigit = cpf[0];

            foreach (char digit in cpf)
            {
                if (digit != firstDigit)
                {
                    return false;
                }
            }

            return true;
        }
    }

}
