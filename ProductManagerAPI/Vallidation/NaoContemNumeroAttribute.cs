using System.ComponentModel.DataAnnotations;

namespace ProductManagerAPI.Vallidation
{
    public class NaoContemNumeroAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && value.ToString().Any(char.IsDigit))
            {
                return new ValidationResult("O nome de usuário não pode conter números.");
            }

            return ValidationResult.Success;
        }
    }
}
