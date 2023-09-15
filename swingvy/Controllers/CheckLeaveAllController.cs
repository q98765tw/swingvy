using Microsoft.AspNetCore.Mvc;

namespace swingvy.Controllers
{
    public class CheckLeaveAllController : Controller
    {
        public IActionResult Index()
        {
            string? member_id = Request.Cookies["member_id"];
            string? member_head = Request.Cookies["member_head"];
            if (member_id != member_head) { return RedirectToAction("Privacy", "Home"); }
            return View();
        }
    }
}
