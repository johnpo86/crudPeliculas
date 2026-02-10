using System.ComponentModel.DataAnnotations;

namespace Api.Validations
{
    public class PositivePriceAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is decimal price && price > 0)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("El precio debe ser un valor positivo mayor a cero.");
        }
    }
}
