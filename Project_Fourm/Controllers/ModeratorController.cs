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


        public IActionResult Remove()
        {
            var reportedContent = Context.ReportedContents.ToList();
            return View(reportedContent);
        }

        public IActionResult Keep()
        {
            var reportedContent = Context.ReportedContents.ToList();
            return View(reportedContent);
        }
    }
}
