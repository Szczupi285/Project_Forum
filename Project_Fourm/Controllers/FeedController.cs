using Microsoft.AspNetCore.Mvc;

namespace Project_Forum.Controllers
{
    public class FeedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
