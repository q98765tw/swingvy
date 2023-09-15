using Microsoft.AspNetCore.Mvc;

namespace swingvy.Controllers
{
    public class EmployeeListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
