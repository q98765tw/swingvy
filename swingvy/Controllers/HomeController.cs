using Microsoft.AspNetCore.Mvc;
using swingvy.Models;
using System.Diagnostics;

namespace swingvy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly swingvyContext _swingvyContext;
        public HomeController(swingvyContext context)
        {
            _swingvyContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var membername = from m in _swingvyContext.memberData
                             select m.name;

            ViewBag.Name = membername.ToList();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}