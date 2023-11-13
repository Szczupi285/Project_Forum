﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Forum.Models;
using Project_Forum.Services;

namespace Project_Forum.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRegisterService RegisterService;
        private readonly ILoginService LoginService;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;

        public AccountController(IRegisterService registerService, ILoginService loginService,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
            ,ILogger<AccountController> logger, IHttpContextAccessor httpContextAccessor)
        {
            RegisterService = registerService;
            UserManager = userManager;
            SignInManager = signInManager;
            LoginService = loginService;
            _logger = logger;
            _contextAccessor = httpContextAccessor;
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
                LoginService loginService = new LoginService(UserManager, SignInManager);


                bool result = await loginService.ValidateCreditentials(model);
                if(result == true)
                {
                    await loginService.EstablishSession(_contextAccessor, model);
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
                bool result = await RegisterService.RegisterUser(UserManager, model,ModelState);
                    
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
