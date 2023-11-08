﻿using Microsoft.AspNetCore.Identity;
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

            public AccountController(IRegisterService registerService, ForumProjectContext projectContext, UserManager<ApplicationUser> userManager)
            {
                RegisterService = registerService;
                this.ProjectContext = projectContext;
                UserManager = userManager;
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
