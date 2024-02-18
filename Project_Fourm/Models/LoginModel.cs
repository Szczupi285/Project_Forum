using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Project_Forum.Models
{
    public class LoginModel
    {
        [StringLength(maximumLength: 36)]
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^[a-zA-Z0-9]{3,36}$", ErrorMessage = "Username can't contain special characters")]
        public string Username { get; set; }

        [StringLength(100, MinimumLength = 10,
           ErrorMessage = "Passwod must be at least 10 characters long")]
        [RegularExpression(@"^(?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9])(?=\S*?[#?!@$%^&*-]).{10,}$",
           ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
      
    }
}
