using Microsoft.AspNetCore.Mvc;
using Project_Forum.Models;
using Project_Forum.Models.Entities;
using Project_Forum.Services.Moderator;
using System.Security.Claims;

namespace Project_Forum.Controllers
{
    public class ModeratorController : Controller
    {

        private readonly ForumProjectContext Context;

        private readonly IModeratorService ModeratorService;

        public ModeratorController(ForumProjectContext context, IModeratorService moderatorService)
        {
            Context = context;
            ModeratorService = moderatorService;
        }
        public async Task<IActionResult> Index(List<ReportDisplayContent> reportDisplayContent)
        {
            reportDisplayContent = await ModeratorService.RetriveNotSolvedReports();
            return View(reportDisplayContent);
        }


        public async Task<IActionResult> Remove(int reportId, int contentId, string contentType)
        {

            await ModeratorService.Delete(reportId, contentId, User.FindFirstValue("UserId"), contentType);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Keep(int reportId)
        {
            await ModeratorService.Keep(reportId, User.FindFirstValue("UserId"));
            return RedirectToAction("Index");
        }
    }
}
