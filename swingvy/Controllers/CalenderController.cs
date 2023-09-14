using Microsoft.AspNetCore.Mvc;

namespace swingvy.Controllers
{
    public class CalenderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
