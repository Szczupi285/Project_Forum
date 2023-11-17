using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Project_Forum.Models;
using System.Security.Claims;

namespace Project_Forum.Services
{
    public class PostService : IPostService
    {

        private readonly ForumProjectContext Context;
        private readonly UserManager<ApplicationUser> UserManager;

        public PostService(ForumProjectContext context, UserManager<ApplicationUser> userManager)
        {
            Context = context;
            UserManager = userManager;
        }


        public async Task AddPostAsync(string userId, string postContent)
        {
           
            var _Post = new Post
            {
                UserId = userId,
                PostContent = postContent,
            };

            await Context.AddAsync(_Post);
            await Context.SaveChangesAsync();
        }


        public Task AddTagsAsync()
        {
            throw new NotImplementedException();
        }

        public Task AddUpvoteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
