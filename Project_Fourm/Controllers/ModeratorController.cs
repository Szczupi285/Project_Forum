using Microsoft.AspNetCore.Mvc;
using Project_Forum.Models.Entities;

namespace Project_Forum.Controllers
{
    public class ModeratorController : Controller
    {
        public IActionResult Index(ReportedContent reportedContent)
        {
            return View();
        }
    }
}
