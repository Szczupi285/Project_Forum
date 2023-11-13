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
        private readonly ILoginService LoginService;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IRegisterService registerService, ForumProjectContext projectContext,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
            ,ILoginService loginService, ILogger<AccountController> logger)
        {
            RegisterService = registerService;
            this.ProjectContext = projectContext;
            UserManager = userManager;
            SignInManager = signInManager;
            LoginService = loginService;
            _logger = logger;
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
                bool result = await LoginService.SignIn(UserManager, SignInManager, model);
                if(result == true)
                {
                    return RedirectToAction("Index", "Forum");
                }
                else
                {
                    ModelState.AddModelError("Username", "Wrong login or password");
                    return View(model);
                }

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
