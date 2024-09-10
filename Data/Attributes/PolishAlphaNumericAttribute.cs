﻿using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CustomersRepo.Data.Attributes
{
    public class PolishAlphanumericAttribute : ValidationAttribute
    {
        private static readonly Regex PolishAlphanumericRegex = new Regex(
            @"^[a-zA-Z0-9ąćęłńóśźżĄĆĘŁŃÓŚŹŻ\s.]+$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var stringValue = value as string;
            if (stringValue != null && PolishAlphanumericRegex.IsMatch(stringValue))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("The field contains invalid characters. Only Polish alphanumeric characters are allowed.");
        }
    }
}
