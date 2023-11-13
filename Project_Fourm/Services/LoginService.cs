using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Project_Forum.Models;

namespace Project_Forum.Services
{
    public class LoginService : ILoginService
    {
        public async Task<bool> SignIn(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            return result.Succeeded;

        }
    }
}
