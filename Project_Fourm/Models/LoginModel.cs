using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Project_Forum.Models
{
    public class LoginModel
    {
        [StringLength(maximumLength: 36, MinimumLength = 1)]
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Username cannot contain spaces")]
        public string Username { get; set; }

        [MinLength(1)]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Password cannot contain spaces")]
        public string Password { get; set; }
      
    }
}
