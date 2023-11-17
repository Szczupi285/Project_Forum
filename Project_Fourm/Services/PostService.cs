using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Project_Forum.Models;
using System.Security.Claims;

namespace Project_Forum.Services
{
    public class PostService : IPostService
    {
        public Task AddPostAsync()
        {
            throw new NotImplementedException();
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
