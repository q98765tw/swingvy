using Microsoft.AspNetCore.Mvc;
using swingvy.Models;

namespace swingvy.Controllers
{
    // 定義一個模型類別來組織資料
    public class CalendarViewModel
    {
        public string MemberName { get; set; }
        public string MemAct { get; set; }
        public string StTime { get; set; }
        public string EdTime { get; set; }
    }

    // 定義一個服務類別來處理行事曆的資料查詢
    public class CalendarService
    {
        private readonly swingvyContext _swingvyContext;

        public CalendarService(swingvyContext swingvyContext)
        {
            _swingvyContext = swingvyContext;
        }

        public List<CalendarViewModel> GetCalendarData()
        {
            return (from w in _swingvyContext.calendar
                    join m in _swingvyContext.memberData on w.member_id equals m.member_id
                    select new CalendarViewModel
                    {
                        MemberName = m.name,
                        MemAct = w.name,
                        StTime = Convert.ToDateTime(w.startTime).ToString("yyyy-MM-dd"),
                        EdTime = Convert.ToDateTime(w.endTime).ToString("yyyy-MM-dd"),
                    }).ToList();
        }
    }

    public class CalenderController : Controller
    {
        private readonly CalendarService _calendarService;

        public CalenderController(CalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        public IActionResult Index()
        {
            var calendarData = _calendarService.GetCalendarData();
            ViewBag.CalData = calendarData;
            return View();
        }

        //private readonly swingvyContext _swingvyContext;
        //public CalenderController(swingvyContext context)
        //{
        //    _swingvyContext = context;
        //}


        //public IActionResult Index()
        //{
        //    var Cal = from w in _swingvyContext.calendar
        //              join m in _swingvyContext.memberData
        //              on w.member_id equals m.member_id
        //              select new
        //              {
        //                  MemberName = m.name,
        //                  memAct = w.name,
        //                  StTime = Convert.ToDateTime(w.startTime).ToString("yyyy-MM-dd"),
        //                  EdTime = Convert.ToDateTime(w.endTime).ToString("yyyy-MM-dd"),
        //              };
        //    ViewBag.CalData = Cal.ToList();
        //    return View();
        //}

    }
}
