using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project_Forum.Models;
using Project_Forum.Models.Entities;
using System.Security.Claims;

namespace Project_Forum.Services.Login
{
    public class LoginService : ILoginService
    {
        UserManager<ApplicationUser> UserManager { get; }
        SignInManager<ApplicationUser> SignInManager { get; }



        public LoginService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public async Task<bool> ValidateCreditentials(LoginModel model)
        {
            var user = await UserManager.FindByNameAsync(model.Username);
            if(user is not null)
            {
                var result = await SignInManager.CheckPasswordSignInAsync(user, model.Password, false);
                return result.Succeeded;
            }
            return false;

        }


        public async Task EstablishSession(IHttpContextAccessor httpContextAccessor, LoginModel model)
        {
            var user = await UserManager.FindByNameAsync(model.Username);

            var roles = await UserManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
            new Claim("Username", user.UserName),
            new Claim("Email", user.Email),
            new Claim("UserId", user.Id),
            };
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                IssuedUtc = DateTimeOffset.UtcNow,
                // The time at which the authentication ticket was issued.

            };


            await httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
        }

    }
}
