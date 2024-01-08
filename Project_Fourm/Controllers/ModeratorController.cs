using Microsoft.AspNetCore.Mvc;
using Project_Forum.Models;
using Project_Forum.Models.Entities;
using Project_Forum.Services;

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


        public IActionResult Remove(ReportedContent reportedContent)
        {
            return View(reportedContent);
        }

        public IActionResult Keep(ReportedContent reportedContent)
        {
            return View(reportedContent);
        }
    }
}
