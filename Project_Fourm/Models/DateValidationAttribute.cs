using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Project_.Models
{
    public class DateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            

            if (value is not null)
            {
                
                DateTime dateOfBirth = (DateTime)value;

                if (dateOfBirth < DateTime.Now)
                    return ValidationResult.Success;
                else
                    return new ValidationResult("Invalid date");
                    
            }
            return new ValidationResult("Invalid date");

        }
    }
}
