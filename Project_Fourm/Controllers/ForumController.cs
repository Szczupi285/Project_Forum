using Microsoft.AspNetCore.Mvc;
using Project_Fourm.Models;
using System.Diagnostics;

namespace Project_Fourm.Controllers
{
    public class ForumController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
