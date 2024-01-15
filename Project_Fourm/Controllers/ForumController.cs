using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mono.TextTemplating;
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
                model.PostDisplayContents.Add((post, respond));
            }

            // if (User.IsInRole("Admin"))
            //   return View("AdminIndex");
            if (User.IsInRole("Moderator"))
                return RedirectToAction("Index", "Moderator");
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
                // redirect to same page we're currently on 
                string referringUrl = HttpContext.Request.Headers["Referer"].ToString();
                return Redirect(referringUrl);
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

                // redirect to same page we're currently on 
                string referringUrl = HttpContext.Request.Headers["Referer"].ToString();
                return Redirect(referringUrl);
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
        public async Task<IActionResult> DeletePost(int postId)
        {
            if (User.FindFirstValue("UserId") is not null)
            {
                await PostService.RemovePost(postId);

                // redirect to same page we're currently on 
                string referringUrl = HttpContext.Request.Headers["Referer"].ToString();
                return Redirect(referringUrl);

            }
            return NoContent();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteRespond(int respondId)
        {
            if (User.FindFirstValue("UserId") is not null)
            {
                await PostService.RemoveRespond(respondId);

                // redirect to same page we're currently on 
                string referringUrl = HttpContext.Request.Headers["Referer"].ToString();
                return Redirect(referringUrl);

            }
            return NoContent();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Report(int contentId, string reportReason, string contentType)
        {
            var userId = (User.FindFirstValue("UserId"));
            if (userId is not null)
            {
                await PostService.ReportContent(contentId, userId, reportReason, contentType);

                // redirect to same page we're currently on 
                string referringUrl = HttpContext.Request.Headers["Referer"].ToString();
                return Redirect(referringUrl);
            }
            return NoContent();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Edit(int contentId, string newContent, string contentType)
        {
            var userId = (User.FindFirstValue("UserId"));
            if (userId is not null)
            {
                await PostService.EditContent(contentId, newContent, contentType);
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Tag(PostCompositeModel model, string tag)
        {

          
            // Assigning the value here so we only use FindFirstValue once instead of once per post/respond in foreach loop
            model.CurrentUserId = User.FindFirstValue("UserId");
            var date = model.FilterPostsModel.GetDateDiffFromCurrentDate();
            var posts = await PostService.RetrivePostsByTag(15, date, tag);
            foreach (var post in posts)
            {
                var respond = await PostService.RetriveRespondContentAsync(post.PostId);
                model.PostDisplayContents.Add((post, respond));
            }
            if (User.IsInRole("User"))
                return View("Tag", model);
            else
                return View("GuestTag", model);
        }
        [HttpGet]
        public async Task<IActionResult> Feed(PostCompositeModel model)
        {


            // Assigning the value here so we only use FindFirstValue once instead of once per post/respond in foreach loop
            model.CurrentUserId = User.FindFirstValue("UserId");
            var date = model.FilterPostsModel.GetDateDiffFromCurrentDate();
            var posts = await PostService.RetriveFeed(15, date, User.FindFirstValue("UserId"));
            foreach (var post in posts)
            {
                var respond = await PostService.RetriveRespondContentAsync(post.PostId);
                model.PostDisplayContents.Add((post, respond));
            }
            return View("Feed", model);
           
        }



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
