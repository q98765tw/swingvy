using Microsoft.AspNetCore.Mvc;
using swingvy.Models;
using swingvy.Repositories;

namespace swingvy.Controllers
{
    public class CalenderController : Controller
    {
        private readonly CalendarPageRepository _calendarService;

        public CalenderController(CalendarPageRepository calendarService)
        {
            _calendarService = calendarService;
        }

        public IActionResult Index()
        {
            var calendarData = _calendarService.GetCalendarData();
            ViewBag.CalData = calendarData;
            return View();
        }

    }
}
