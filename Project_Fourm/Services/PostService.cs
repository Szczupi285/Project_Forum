using Azure;
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


        public async Task<int> AddPostAsync(string userId, string postContent)
        {
           
            var _Post = new Post
            {
                UserId = userId,
                PostContent = postContent,
            };

            await Context.AddAsync(_Post);
            await Context.SaveChangesAsync();

            return _Post.PostId;
        }

       

        public async Task<HashSet<string>> AddTagsAsync(string postContent)
        {
            var Tags = TagExtractor.ExtractTags(postContent);

            foreach (string tag in Tags)
            {
                var ExistingTag = await Context.Tags.FindAsync(tag);

                if(ExistingTag is null)
                {
                    Tag NonExistingTag = new Tag
                    {
                        TagName = tag,
                    };

                    await Context.Tags.AddAsync(NonExistingTag);
                }

            }
            await Context.SaveChangesAsync();
            return Tags;    

        }

        public async Task<HashSet<(int, string)>> AddPostTagsAsync(int postId, HashSet<string> tags)
        {

            var PostTagPKs = new HashSet<(int, string)>();

            foreach (string tag in tags)
            {
                var ExistingTag = await Context.Tags.FindAsync(tag);

                if (ExistingTag is not null)
                {
                    PostTag postTag = new PostTag
                    {
                        PostId = postId,
                        TagName = tag
                    };

                    var result = await Context.PostTags.AddAsync(postTag);
                    PostTagPKs.Add((postId, tag));
                    
                }
            }
            await Context.SaveChangesAsync();
            return PostTagPKs;
        }
            

        public Task AddUpvoteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
