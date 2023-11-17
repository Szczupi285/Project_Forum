using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_.Models;
using Project_Forum.Models;
using Project_Forum.Services;
using System.Diagnostics;

namespace Project_Forum.Controllers
{
    public class ForumController : Controller
    {
        private readonly ForumProjectContext Context;

        private readonly SignInManager<ApplicationUser> SignInManager;

        private readonly IPostService PostService;

        public ForumController(ForumProjectContext context, SignInManager<ApplicationUser> signInManager, PostService postService)
        {
            this.Context = context;
            SignInManager = signInManager;

        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return View("UserIndex");
            else
                return View("GuestIndex");
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public async Task CreatePost()
        {
            await PostService.AddPostAsync();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
