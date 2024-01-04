using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Project_.Models;
using Project_Forum.Models;
using Project_Forum.Models.Entities;
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

        public async Task<IActionResult> Index(PostCompositeModel model)
        {
            // Assigning the value here so we only use FindFirstValue once instead of once per post/respond in foreach loop
            model.CurrentUserId = User.FindFirstValue("UserId");

            var date = model.FilterPostsModel.GetDateDiffFromCurrentDate();
            var posts = await PostService.RetrivePostContentAsync(15, date);
            foreach (var post in posts)
            {
                var respond = await PostService.RetriveRespondContentAsync(post.PostId);
                model.PostDisplayContents.Add((post,respond));
            }

            if (User.IsInRole("Admin"))
                return View("AdminIndex");
            else if (User.IsInRole("Moderator"))
                return View("ModeratorIndex");
            else if (User.IsInRole("User"))
                return View("UserIndex", model);
            else
                return View("GuestIndex", model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost(PostCompositeModel model)
        {
            // we don't use ModelState.IsValid because FK User is not Explicitly set in model
            if (User.FindFirstValue("UserId") is not null && !String.IsNullOrEmpty(model.PostModel.PostContent))
            {
                int postId = await PostService.AddPostAsync(User.FindFirstValue("UserId"), model.PostModel.PostContent);
                var tags = await PostService.AddTagsAsync(model.PostModel.PostContent);
                await PostService.AddPostTagsAsync(postId, tags);
                return RedirectToAction("Index", "Forum");
            }
            else
            {
                return NoContent();
            }
                
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ManageUpvote(int postId)
        {
            if (User.FindFirstValue("UserId") is not null)
            {
                await PostService.ManageUpvoteAsync(User.FindFirstValue("UserId"), postId);
                return StatusCode(201);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRespond(PostCompositeModel model, int postId)
        {
            // we don't use ModelState.IsValid because FK User is not Explicitly set in model
            if (User.FindFirstValue("UserId") is not null && !String.IsNullOrEmpty(model.RespondModel.RepondContent))
            {
                await PostService.AddRespondAsync(postId, User.FindFirstValue("UserId"), model.RespondModel.RepondContent);
                return RedirectToAction("Index", "Forum");
            }
            else
            {
                return NoContent();
            }

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ManageRespondUpvote(int respondId)
        {
            if (User.FindFirstValue("UserId") is not null)
            {
                await PostService.ManageRespondUpvoteAsync(User.FindFirstValue("UserId"), respondId);
                return StatusCode(201);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> HandlePostButtons(string action)
        {
            if (User.FindFirstValue("UserId") is not null)
            {
                switch (action)
                {
                    case "DeletePost":
                        throw new NotImplementedException();
                    case "ReportPost":
                        throw new NotImplementedException();
                    case "DeleteRespond":
                        throw new NotImplementedException();
                    case "ReportRespond":
                        throw new NotImplementedException();
                        default: throw new NotImplementedException();

                }
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
