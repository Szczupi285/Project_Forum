using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Project_Forum.Models;
using Project_Forum.Models.Entities;

namespace Project_Forum.Services.Register
{
    public interface IRegisterService
    {
        Task<bool> RegisterUser(UserManager<ApplicationUser> userManager, RegisterModel model, ModelStateDictionary modelState);
    }
}
