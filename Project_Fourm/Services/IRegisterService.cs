using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Project_Forum.Models;

namespace Project_Forum.Services
{
    public interface IRegisterService
    {
        Task<bool> RegisterUser(UserManager<ApplicationUser> userManager, RegisterModel model, ModelStateDictionary modelState);
    }
}
