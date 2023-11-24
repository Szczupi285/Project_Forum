using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_.Models;
using Project_Forum.Models;
using Project_Forum.Services;
using System.Diagnostics;
using System.Security.Claims;

namespace Project_Forum.Controllers
{
    public class ForumController : Controller
    {
        private readonly ForumProjectContext Context;

        private readonly SignInManager<ApplicationUser> SignInManager;

        private readonly IPostService PostService;

        public ForumController(ForumProjectContext context, SignInManager<ApplicationUser> signInManager, IPostService postService)
        {
            this.Context = context;
            SignInManager = signInManager;
            PostService = postService;
        }

        public IActionResult Index()
        {
            if (User.Identity is not null && User.Identity.IsAuthenticated)
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
        public async Task<IActionResult> CreatePost(Post model)
        {
            // we don't use ModelState.IsValid because FK User is not Explicitly set in model
            if (User.FindFirstValue("UserId") is not null && !String.IsNullOrEmpty(model.PostContent))
            {
                int postId = await PostService.AddPostAsync(User.FindFirstValue("UserId"), model.PostContent);
                var tags = await PostService.AddTagsAsync(model.PostContent);
                await PostService.AddPostTagsAsync(postId, tags);
                return RedirectToAction("Index", "Forum");
            }
            else
            {
                return NoContent();
            }
                
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
