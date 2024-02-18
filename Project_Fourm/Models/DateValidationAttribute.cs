using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Project_.Models
{
    public class DateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value is not null && !String.IsNullOrEmpty(value.ToString()))
            {

                DateTime dateOfBirth;
                bool isDateTime = DateTime.TryParse(value.ToString(), out dateOfBirth);

                if (isDateTime == true && dateOfBirth < DateTime.Now && dateOfBirth >= DateTime.Parse("1900.01.01"))
                    return ValidationResult.Success;
                else
                    return new ValidationResult("Invalid date");
                    
            }
            else
            {
                return new ValidationResult("Invalid date");
            }

        }
    }
}
