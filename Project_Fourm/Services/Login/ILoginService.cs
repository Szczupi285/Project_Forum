using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Project_Forum.Models;

namespace Project_Forum.Services.Login
{
    public interface ILoginService
    {
        // "Models" prepended because of ambiguous reference
        Task<bool> ValidateCreditentials(Models.LoginModel model);

        public Task EstablishSession(IHttpContextAccessor httpContextAccessor, Models.LoginModel model);
    }
}
