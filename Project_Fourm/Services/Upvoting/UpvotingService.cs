using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_Forum.Models.Entities;

namespace Project_Forum.Services.Upvoting
{
    public class UpvotingService : IUpvotingService
    {

        private readonly ForumProjectContext Context;
        private readonly UserManager<ApplicationUser> UserManager;

        public UpvotingService(ForumProjectContext context, UserManager<ApplicationUser> userManager)
        {
            Context = context;
            UserManager = userManager;
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
            if (existingUpvote is null)
                await Context.PostUpvotes.AddAsync(new PostUpvote { UserId = userId, PostId = postId });
            else
                Context.PostUpvotes.Remove(existingUpvote);
            await Context.SaveChangesAsync();
        }
    }
}
