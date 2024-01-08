using Microsoft.AspNetCore.Mvc;
using Project_Forum.Models.Entities;

namespace Project_Forum.Controllers
{
    public class ModeratorController : Controller
    {

        private readonly ForumProjectContext Context;

        public ModeratorController(ForumProjectContext context)
        {
            Context = context;
        }
        public IActionResult Index()
        {
            var reportedContent = Context.ReportedContents.ToList();
            return View(reportedContent);
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
