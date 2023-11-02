using System.ComponentModel.DataAnnotations;

namespace Project_Fourm.Models
{
    public class DateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is DateOnly)
            {
                DateTime dateTime = (DateTime)value;

                if (dateTime < DateTime.Now)
                    return ValidationResult.Success;
                else
                    return new ValidationResult("Invalid date");
                    
            }
            return new ValidationResult("Invalid date");

        }
    }
}
