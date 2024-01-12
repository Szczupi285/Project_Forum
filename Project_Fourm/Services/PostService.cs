using Azure;
using Azure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project_Forum.Models;
using Project_Forum.Models.Entities;
using System.Diagnostics;
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
                    await Context.Tags.AddAsync(new Tag { TagName = tag});
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

        /// <summary>
        /// Manages user upvotes for a post asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the user that performs upvote operation.</param>
        /// <param name="postId">The unique identifier of the post to be upvoted.</param>
        ///  /// <remarks>
        /// Method checks if user already upvoted given post.
        /// If so it removes the upvote. In other case it adds PostUpvote to the database.
        /// </remarks>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task ManageUpvoteAsync(string userId, int postId)
        {
            var existingUpvote = await Context.PostUpvotes.FirstOrDefaultAsync
                (up => up.UserId == userId && up.PostId == postId);
            if(existingUpvote is null)
               await Context.PostUpvotes.AddAsync(new PostUpvote { UserId = userId, PostId = postId });
            else
                Context.PostUpvotes.Remove(existingUpvote);
                await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves a specified number of posts that has been created since (showPostSince) asynchronously.
        /// </summary>
        /// <param name="numberOfPosts">The maximum number of posts to retrieve.</param>
        /// <param name="showPostSince">The datetime representing the starting point in time for post retrieval.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result is a list of PostDisplayContent objects,
        /// containing user information, post content, creation date, and the number of upvotes for each post.
        /// </returns>
        /// <remarks>
        /// This method performs a query to retrieve post content, user information, creation date, upvote count, and post ID
        /// from the database based on specified criteria, such as the number of posts and the starting point in time.
        /// The result is ordered by the count of upvotes in descending order.
        /// </remarks>
        public async Task<List<PostDisplayContent>> RetrivePostContentAsync(int numberOfPosts, DateTime showPostSince)
        {


            var query =
                 (from Post post in Context.Posts
                  join ApplicationUser users in Context.AspNetUsers on post.UserId equals users.Id
                  join PostUpvote postUpovtes in Context.PostUpvotes on post.PostId equals postUpovtes.PostId
                  into UpvGrp
                  where post.CreatedAt > showPostSince
                  orderby UpvGrp.Count() descending
                  select new PostDisplayContent
                  (
                     users.UserName,
                     post.PostContent,
                     post.CreatedAt,
                     UpvGrp.Count(),
                     post.PostId,
                     post.UserId
                  )).Take(numberOfPosts);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specified number of posts
        /// </summary>
        /// <param name="numberOfPosts">The maximum number of posts to retrieve.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result is a list of PostDisplayContent objects,
        /// containing user information, post content, creation date, and the number of upvotes for each post.
        /// </returns>
        /// <remarks>
        /// This method retrieves posts along with relevant display content, such as user information, post content,
        /// creation date, and the count of upvotes. 
        /// The result is ordered by the descending count of upvotes, and the maximum number of
        /// posts returned is determined by the 'numberOfPosts' parameter.
        /// </remarks>
        public async Task<List<PostDisplayContent>> RetrivePostContentAsync(int numberOfPosts)
        {


            var query =
                 (from Post post in Context.Posts
                  join ApplicationUser users in Context.AspNetUsers on post.UserId equals users.Id
                  join PostUpvote postUpovtes in Context.PostUpvotes on post.PostId equals postUpovtes.PostId
                  into UpvGrp
                  orderby UpvGrp.Count() descending
                  select new PostDisplayContent
                  (
                     users.UserName,
                     post.PostContent,
                     post.CreatedAt,
                     UpvGrp.Count(),
                     post.PostId,
                     post.UserId
                  )).Take(numberOfPosts);

            return await query.ToListAsync();
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

        /// <summary>
        /// Manages user upvotes for a respond asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the user that performs upvote operation.</param>
        /// <param name="postId">The unique identifier of the respond to be upvoted.</param>
        /// <remarks>
        /// Method checks if user already upvoted given respond.
        /// If so it removes the upvote. In other case it adds RespondUpvote to the database.
        /// </remarks>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task ManageRespondUpvoteAsync(string userId, int respondId)
        {
            var existingUpvote = await Context.RespondUpvotes.FirstOrDefaultAsync
                (up => up.UserId == userId && up.RespondId == respondId);
            if (existingUpvote is null)
                await Context.RespondUpvotes.AddAsync(new RespondUpvote { UserId = userId, RespondId = respondId });
            else
                Context.RespondUpvotes.Remove(existingUpvote);
            await Context.SaveChangesAsync();
        }


        /// <summary>
        /// Retrieves responds of given post
        /// </summary>
        /// <param name="postId">Id of post for which we want to retrive responds</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result is a list of RespondDisplayContent objects,
        /// containing user information, respond content, respond Id, creation date, Id of post respond is reffering to, and the number of upvotes for each post.
        /// </returns>
        /// <remarks>
        /// This method retrieves respond along with relevant display content, such as user information, respond content,
        /// respond Id, Id of post respond is refeering to, creation date, and the count of upvotes. 
        /// </remarks>
        public async Task<List<RespondDisplayContent>> RetriveRespondContentAsync(int postId)
        {


            var query =
                 (from Respond respond in Context.Responds
                  join ApplicationUser users in Context.AspNetUsers on respond.UserId equals users.Id
                  join RespondUpvote respondUpovtes in Context.RespondUpvotes on respond.RespondId equals respondUpovtes.RespondId
                  into UpvGrp
                  orderby UpvGrp.Count() descending
                  where respond.PostId == postId
                  select new RespondDisplayContent
                  (
                     users.UserName,
                     respond.RepondContent,
                     respond.CreatedAt,
                     UpvGrp.Count(),
                     respond.RespondId,
                     respond.PostId,
                     respond.UserId
                  ));

            return await query.ToListAsync();
        }

        #endregion

        #region ActionButtons

        // on delete modificator in database is cascade so
        // removing Post will also remove every upvote/respond/respondUpvote/PostTags
        public async Task<bool> RemovePost(int postId)
        {
            var post = await Context.Posts.FindAsync(postId);

            if(post is not null)
            {
                Context.Posts.Remove(post);
                Context.SaveChanges();
                return true;
            }
            return false;
               
        }

        // on delete modificator in database is cascade so removing Respond will also remove every respondUpvote

        public async Task<bool> RemoveRespond(int respondId)
        {
            var respond = await Context.Responds.FindAsync(respondId);

            if (respond is not null)
            {
                Context.Responds.Remove(respond);
                Context.SaveChanges();
                return true;
            }
            return false;
        }


        public async Task<bool> ReportContent(int contentId, string submitterId, string reason, string contentType)
        {
            if (reason.Length > 200)
                return false;

            // checking if submitter already reported this content
            var matchingReport = Context.ReportedContents
                   .Where(rc => rc.ContentId == contentId
                   && rc.SubmitterId == submitterId && rc.ContentType == contentType);

            if (matchingReport.Any())
                return false;


            string userId = "";
            string content = "";

            if (contentType == "Post")
            {
                    var post = await Context.Posts.FindAsync(contentId);
                    if (post is not null)
                    {
                        userId = post.UserId;
                        content = post.PostContent;
                    }
            }
            else if (contentType == "Respond")
            {
                var respond = await Context.Responds.FindAsync(contentId);

                if(respond is not null)
                {
                    userId = respond.UserId;
                    content = respond.RepondContent;
                }
                
            }
            else return false;

            if (!String.IsNullOrEmpty(userId) || !String.IsNullOrEmpty(content))
            {
                var repContent = new ReportedContent
                {
                    ReportedUserId = userId,
                    SubmitterId = submitterId,
                    ContentId = contentId,
                    ContentType = contentType,
                    IsResolved = false,
                    Reason = reason,
                    Content = content,
                    ReportDate = DateTime.Now
                    
                };
                await Context.ReportedContents.AddAsync(repContent);
                await Context.SaveChangesAsync();
                return true;
            }
            return false;
            
            
        }




        public async Task<bool> EditContent(int contentId, string newContent, string contentType)
        {
            if (contentType == "Post")
            {
                var post = await Context.Posts.FindAsync(contentId);
                if (post is not null)
                {
                    post.PostContent = newContent;
                    await Context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            else if (contentType == "Respond")
            {
                var respond = await Context.Responds.FindAsync(contentId);
                if (respond is not null)
                {
                    respond.RepondContent = newContent;
                    await Context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        #endregion

        #region Search

        public async Task<List<PostDisplayContent>> RetrivePostsByTag(int numberOfPosts, DateTime showPostSince, string tag)
        {
            Tag tagEntity = new Tag
            {
                TagName = tag
            };

            var query =
                 (from Post post in Context.Posts
                  join ApplicationUser users in Context.AspNetUsers on post.UserId equals users.Id
                  join PostUpvote postUpovtes in Context.PostUpvotes on post.PostId equals postUpovtes.PostId
                  into UpvGrp
                  where post.CreatedAt > showPostSince && 
                  post.TagNames.Contains(tagEntity)
                  orderby UpvGrp.Count() descending
                  select new PostDisplayContent
                  (
                     users.UserName,
                     post.PostContent,
                     post.CreatedAt,
                     UpvGrp.Count(),
                     post.PostId,
                     post.UserId
                  )).Take(numberOfPosts);

            return await query.ToListAsync();
        }

        #endregion
    }
}
