using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Project_Forum.Models;
using Project_Forum.Models.Entities;
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


        /// <summary>
        /// Asynchronously adds a new post to the database.
        /// </summary>
        /// <param name="userId">The identifier of the user creating the post.</param>
        /// <param name="postContent">The content of the post.</param>
        /// <returns>
        /// An integer representing the unique identifier (PostId) assigned to the newly added post.
        /// </returns>
        /// <remarks>
        /// Creates a new post entity with the provided user ID and post content, <br></br>
        /// adds it to the database, and asynchronously saves the changes.
        /// </remarks>
        /// <returns>The unique identifier (PostId) assigned to the newly added post.</returns>
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


        /// <summary>
        /// extracts and Asynchronously adds tags from the given post content to the database.
        /// </summary>
        /// <param name="postContent">The content of the post from which tags are to be extracted.</param>
        /// <returns>
        /// A HashSet of strings representing the extracted tags.
        /// </returns>
        /// <remarks>
        /// This method uses a tag extractor utility to identify and extract tags from the provided post content.
        /// It then checks if each tag already exists in the database and adds non-existing tags. <br></br>
        /// </remarks>
        /// <returns>A HashSet of strings representing the extracted tags.</returns>
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

        /// <summary>
        /// Asynchronously adds associations between a post and its corresponding tags to the database.
        /// </summary>
        /// <param name="postId">The unique identifier of the post.</param>
        /// <param name="tags">A HashSet of strings representing the tags to associate with the post.</param>
        /// <returns>
        /// A HashSet of tuples representing the unique identifiers (PostId, TagName) assigned to the added associations.
        /// </returns>
        /// <remarks>
        /// Method takes a Post identifier and a HashSet of tags, checks if each tag exists in the database,
        /// and creates associations between the post and existing tags.
        /// It returns a HashSet of tuples representing the composite key of the added associations
        /// and saves changes to the database.
        /// </remarks>
        /// <returns>
        /// A HashSet of tuples representing the composite key (PostId, TagName) assigned to the added associations.
        /// </returns>
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
