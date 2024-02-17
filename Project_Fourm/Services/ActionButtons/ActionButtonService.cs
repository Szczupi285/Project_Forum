using Microsoft.AspNetCore.Identity;
using Project_Forum.Models.Entities;

namespace Project_Forum.Services.ActionButtons
{
    public class ActionButtonService : IActionButtonsService
    {
        private readonly ForumProjectContext Context;
        private readonly UserManager<ApplicationUser> UserManager;

        public ActionButtonService(ForumProjectContext context, UserManager<ApplicationUser> userManager)
        {
            Context = context;
            UserManager = userManager;
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

        // on delete modificator in database is cascade so
        // removing Post will also remove every upvote/respond/respondUpvote/PostTags
        public async Task<bool> RemovePost(int postId)
        {
            var post = await Context.Posts.FindAsync(postId);

            if (post is not null)
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

                if (respond is not null)
                {
                    userId = respond.UserId;
                    content = respond.RepondContent;
                }

            }
            else return false;

            if (!string.IsNullOrEmpty(userId) || !string.IsNullOrEmpty(content))
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
    }
}
