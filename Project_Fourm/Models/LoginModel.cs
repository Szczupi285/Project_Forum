using System.ComponentModel.DataAnnotations;

namespace Project_Fourm.Models
{
    public class LoginModel
    {
        [StringLength(maximumLength: 36)]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

    }
}
