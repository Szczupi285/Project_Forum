﻿using Azure;
using Azure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Project_Forum.Models;
using Project_Forum.Models.Entities;
using System.Diagnostics;
using System.Security.Claims;

namespace Project_Forum.Services.PostCreation
{
    public class PostCreation : IPostCreationService
    {

        private readonly ForumProjectContext Context;
        private readonly UserManager<ApplicationUser> UserManager;

        public PostCreation(ForumProjectContext context, UserManager<ApplicationUser> userManager)
        {
            Context = context;
            UserManager = userManager;
        }

        #region POSTS
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

                if (ExistingTag is null)
                {
                    await Context.Tags.AddAsync(new Tag { TagName = tag });
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
            var existingPost = await Context.Posts.FindAsync(postId);
            if (existingPost is not null)
            {
                foreach (string tag in tags)
                {
                    var existingTag = await Context.Tags.FindAsync(tag);

                    if (existingTag is not null)
                    {

                        existingPost.TagNames.Add(existingTag);
                        PostTagPKs.Add((postId, tag));

                    }
                }
            }
            await Context.SaveChangesAsync();
            return PostTagPKs;

        }

      

        #endregion

        #region RESPONDS
        /// <summary>
        /// Asynchronously adds a new respond to the database.
        /// </summary>
        /// <param name="postId">The identifier of the post respond is reffering to</param>
        /// <param name="userId">The identifier of the user creating the post.</param>
        /// <param name="respondContent">The content of the respond.</param>
        /// <remarks>
        /// Creates a new respond entity with the provided post ID, user ID and post content, <br></br>
        /// adds it to the database, and asynchronously saves the changes.
        /// </remarks>
        public async Task AddRespondAsync(int postId, string userId, string respondContent)
        {
            var Respond = new Respond
            {
                PostId = postId,
                RepondContent = respondContent,
                UserId = userId,
                CreatedAt = DateTime.Now,

            };

            await Context.AddAsync(Respond);
            await Context.SaveChangesAsync();

        }

        #endregion

    }
}
