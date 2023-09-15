using Microsoft.AspNetCore.Mvc;
using swingvy.Models;

namespace swingvy.Controllers
{
    public class CalenderController : Controller
    {

        private readonly swingvyContext _swingvyContext;
        public CalenderController(swingvyContext context)
        {
            _swingvyContext = context;
        }
        public IActionResult Index()
        {
            var Cal = from w in _swingvyContext.calendar
                      join m in _swingvyContext.memberData
                      on w.member_id equals m.member_id
                      select new
                      {
                          MemberName = m.name,
                          memAct = w.name,
                          StTime = Convert.ToDateTime(w.startTime).ToString("yyyy-MM-dd"),
                          EdTime = Convert.ToDateTime(w.endTime).ToString("yyyy-MM-dd"),
                      };
            ViewBag.CalData = Cal.ToList();
            return View();
        }
    }
}
