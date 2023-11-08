using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Forum.Models;
using Project_Forum.Services;

namespace Project_Forum.Controllers
{
    public class AccountController : Controller
    {
        private readonly ForumProjectContext ProjectContext;
        private readonly IRegisterService RegisterService;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly SignInManager<ApplicationUser> SignInManager;

        public AccountController(IRegisterService registerService, ForumProjectContext projectContext, 
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            RegisterService = registerService;
            this.ProjectContext = projectContext;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Username);

                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Forum");
                }
                ModelState.AddModelError("Username", "Wrong login or password");
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                bool result = await RegisterService.RegisterUser(UserManager, model, ModelState);
                    
                if(result == true) 
                    return RedirectToAction("Login");
                else
                    return View(model);
        }
            else
            {
                return View(model);
            }


        }
    }
}
