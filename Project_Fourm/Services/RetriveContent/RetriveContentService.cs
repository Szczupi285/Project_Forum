using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_Forum.Models;
using Project_Forum.Models.Entities;

namespace Project_Forum.Services.RetriveContent
{
    public class RetriveContentService : IRetriveContentService
    {
        private readonly ForumProjectContext Context;
        private readonly UserManager<ApplicationUser> UserManager;

        public RetriveContentService(ForumProjectContext context, UserManager<ApplicationUser> userManager)
        {
            Context = context;
            UserManager = userManager;
        }

        public async Task<List<PostDisplayContent>> RetriveFeed(int numberOfPosts, DateTime showPostSince, string userId)
        {
            // gets Tags user is subscribedTo
            var SubsribedTags =
                                Context.AspNetUsers.Where(u => u.Id == userId).
                                SelectMany(u => u.TagNames).ToList();

            var query =
                  (from Post post in Context.Posts
                   join ApplicationUser users in Context.AspNetUsers on post.UserId equals users.Id
                   join PostUpvote postUpovtes in Context.PostUpvotes on post.PostId equals postUpovtes.PostId
                   into UpvGrp
                   where post.CreatedAt > showPostSince &&
                   post.TagNames.Any(t => SubsribedTags.Contains(t))
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
                 from Respond respond in Context.Responds
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
                 );

            return await query.ToListAsync();
        }
    }
}
