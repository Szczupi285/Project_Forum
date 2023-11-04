using Microsoft.AspNetCore.Mvc;
using Project_.Models;
using System.ComponentModel.DataAnnotations;

namespace Project_Forum.Models
{
    public class RegisterModel
    {
        [StringLength(maximumLength: 36)]
        [Required(ErrorMessage ="Username is required")]
        public string Username { get; set; }

        [StringLength(100, MinimumLength = 10, 
            ErrorMessage ="Passwod must be at least 10 characters long")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{10,}$", 
            ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [EmailAddress(ErrorMessage ="Invalid E-Mail")]
        [Required(ErrorMessage = "E-Mail is required")]
        [RegularExpression(pattern: @"^[\w\.-]+@[\w\.-]+\.\w+$", ErrorMessage ="Invalid E-Mail")]
        public string Email { get; set; }

        [DateValidation]
        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
