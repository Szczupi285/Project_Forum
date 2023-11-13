using Microsoft.AspNetCore.Identity;
using Project_Forum.Controllers;
using Project_Forum.Models;

namespace Project_Forum.Services
{
    public interface ILoginService
    {
        Task<bool> SignIn(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, LoginModel model);
    }
}
