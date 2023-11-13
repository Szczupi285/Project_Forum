using Microsoft.AspNetCore.Mvc;
using Project_.Models;
using Project_Forum.Models;
using System.Diagnostics;

namespace Project_Forum.Controllers
{
    public class ForumController : Controller
    {
        private readonly ForumProjectContext Context;

        public ForumController(ForumProjectContext context)
        {
            this.Context = context;
        }

        public IActionResult Index(string userId)
        {
            return View();
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            return View();
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
