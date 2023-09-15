using Microsoft.AspNetCore.Mvc;

namespace swingvy.Controllers
{
    public class MemberCenterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
