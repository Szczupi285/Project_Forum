using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Project_Forum.Models;
using Project_Forum.Models.Entities;
using System.Security.Claims;
using System.Transactions;

namespace Project_Forum.Services.Moderator
{
    public class ModeratorService : IModeratorService
    {
        private readonly ForumProjectContext Context;

        public ModeratorService(ForumProjectContext context)
        {
            Context = context;
        }

        public async Task<List<ReportDisplayContent>> RetriveNotSolvedReports()
        {
            var result =
                from reports in Context.ReportedContents
                where reports.IsResolved == false
                select new ReportDisplayContent
                (
                     reports.ReportId,
                     reports.ContentId,
                     reports.Content,
                     reports.ContentType.Trim(),
                     reports.Reason,
                     reports.ReportDate
                );

            return await result.ToListAsync();

        }

        private async Task<bool> DeletePost(int postId)
        {
            var post = await Context.Posts.FindAsync(postId);
            if (post is not null)
            {
                Context.Posts.Remove(post);
                await Context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        private async Task<bool> DeleteRespond(int respondId)
        {
            var respond = await Context.Responds.FindAsync(respondId);
            if (respond is not null)
            {
                Context.Responds.Remove(respond);
                await Context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        private async Task<bool> UpdateReport(int reportId, string moderatorId, string resolution)
        {
            var report = await Context.ReportedContents.FindAsync(reportId);

            if (report is not null)
            {
                report.ModeratorId = moderatorId;
                report.Resolution = resolution;
                report.ResolveDate = DateTime.Now;
                report.IsResolved = true;
                await Context.SaveChangesAsync();
                return true;
            }
            return false;
        }



        public async Task<bool> Delete(int reportId, int contentId, string moderatorId, string contentType)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                bool isDeleted = false;
                if (contentType == "Post")
                    isDeleted = await DeletePost(contentId);
                else if (contentType == "Respond")
                    isDeleted = await DeleteRespond(contentId);

                if (isDeleted)
                {
                    bool isReportUpdated = await UpdateReport(reportId, moderatorId, "Deleted");

                    if (isReportUpdated)
                    {
                        transaction.Complete();
                        return true;
                    }

                }
                return false;
            }

        }

        public async Task<bool> Keep(int reportId, string moderatorId)
        {
            return await UpdateReport(reportId, moderatorId, "Keeped");
        }


    }
}
