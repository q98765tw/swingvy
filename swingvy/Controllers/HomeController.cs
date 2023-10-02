using Microsoft.AspNetCore.Mvc;
using swingvy.Models;
using System.Diagnostics;
using swingvy.Enums;

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

            string userIdStr = Request.Cookies["member_id"];
            int.TryParse(userIdStr, out int userId);
            var Name = from a in _swingvyContext.memberData
                       where a.member_id == userId
                       select new
                       {
                           Name = a.name,
                       };
            ViewBag.UserName = Name.ToList();

            var workTimeRecord = _swingvyContext.worktime.
                FirstOrDefault(wt => wt.member_id == userId && wt.startTime >= today );

            if(workTimeRecord == null)
            {
                var workTimeRecord_2 = _swingvyContext.worktime.FirstOrDefault(n => n.member_id == userId);
                workTimeRecord_2.endTime = null;
                workTimeRecord_2.startTime = null;
                workTimeRecord_2.state = 0;
                _swingvyContext.SaveChanges();
            }

            var workTimeRecord_3 = from w in _swingvyContext.worktime
                                   where w.member_id == userId
                                   select new
                                   {
                                       Start = w.startTime.ToString(),
                                       State = ((WorkState)w.state).ToString(),
                                   };

            ViewBag.worktime = workTimeRecord_3.ToList();
           

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

            ViewBag.Rec_Event = Cal.ToList();
            return View();

        }

        public IActionResult OnDuty()
        {
            string userIdStr = Request.Cookies["member_id"];
            int.TryParse(userIdStr, out int userId);
            var workTimeRecord = _swingvyContext.worktime.FirstOrDefault(w => w.member_id == userId);
            workTimeRecord.startTime = DateTime.Now;
            workTimeRecord.state = 1;
            _swingvyContext.SaveChanges();

            return RedirectToAction("Index","Home");
        }

        public IActionResult GetOff()
        {
            string userIdStr = Request.Cookies["member_id"];
            int.TryParse(userIdStr, out int userId);
            var workTimeRecord = _swingvyContext.worktime.FirstOrDefault(w => w.member_id == userId);
            workTimeRecord.endTime = DateTime.Now;
            workTimeRecord.state = 2;
            _swingvyContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}