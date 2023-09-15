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
        //推送測試
        public IActionResult Index()
        {
            DateTime today = DateTime.Today;
            DateTime sevenDaysLater = today.AddDays(7);

            var Cal = from w in _swingvyContext.calendar
                      join m in _swingvyContext.memberData
                      on w.member_id equals m.member_id
                      where w.startTime >= today && w.startTime <= sevenDaysLater　
                      select new
                      {
                          MemberName = m.name,
                          mem_Act = w.name,
                          Time = w.startTime
                      };
            var Clk = from a in _swingvyContext.worktime
                      where a.member_id == 1
                      select new
                      {
                          WorkStart = a.startTime
                      };
            ViewBag.Rec_Event = Cal.ToList();
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