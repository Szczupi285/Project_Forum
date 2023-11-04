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

            public AccountController(IRegisterService registerService, ForumProjectContext projectContext)
            {
                RegisterService = registerService;
                this.ProjectContext = projectContext;
            }

            [HttpGet]
            public IActionResult Login()
            {
                return View();
            }
            [HttpPost]
            public IActionResult Login(LoginModel model)
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Index", "Forum");
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
                    await RegisterService.RegisterUser(ProjectContext, model.Username, model.Password, model.Email, model.Date);
                    return RedirectToAction("Login");
                }
                else
                {
                    return View(model);
                }


            }
        }
    }
