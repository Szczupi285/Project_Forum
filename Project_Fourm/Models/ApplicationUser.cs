using Microsoft.AspNetCore.Identity;

namespace Project_Forum.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateOfBirth { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? AvatarFilePath { get; set; }

    }
}
