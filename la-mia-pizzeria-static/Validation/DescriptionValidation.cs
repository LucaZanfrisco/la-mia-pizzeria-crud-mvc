using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Validation
{
    public class DescriptionValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            string? description = (string)value;

            if(description == null || description.Split(" ").Length < 5)
            {
                return new ValidationResult("La descrizione deve contenere almeno 5 parole");
            }

            return ValidationResult.Success;

        }
    }
}
