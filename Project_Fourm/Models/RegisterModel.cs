using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Project_Fourm.Models
{
    public class RegisterModel
    {
        [StringLength(maximumLength: 36)]
        [Required(ErrorMessage ="Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [EmailAddress(ErrorMessage ="Wrong format")]
        [Required(ErrorMessage = "E-Mail is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public string Date { get; set; }
    }
}
