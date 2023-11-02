using Microsoft.AspNetCore.Mvc;
using Project_Fourm.Models;
using System.CodeDom;

namespace Project_Fourm.Controllers
{
    public class AccountController : Controller
    {

        private readonly ForumProjectContext Context;

        public AccountController(ForumProjectContext context)
        {
            Context = context;
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
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View(model);
            }


        }
    }
}
