using Microsoft.AspNetCore.Mvc;
using swingvy.Models;
using System.Diagnostics;
using swingvy.Enums;
using swingvy.Services;

namespace swingvy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HomeService _homeService;
        public HomeController(HomeService context)
        {
            _homeService = context;
        }

        //推送測試
        public IActionResult Index()
        {
            DateTime today = DateTime.Today;
            DateTime sevenDaysLater = today.AddDays(7);

            string userIdStr = Request.Cookies["member_id"];
            int.TryParse(userIdStr, out int userId);

            //取得使用者名稱顯示在首頁上頭
            var Name = _homeService.GetUserName(userId);
            ViewBag.UserName = Name;

            var workTimeRecord = _homeService.GetWorkTimeRecord(userId,today); //得到今天的打卡時間資料
            if(workTimeRecord == null)//如果為空代表已經到新的一天了，清除打卡欄位的資料
            {
                _homeService.UpdateWorkTimeRecordForNoState(userId);
            }
            var workTimeRecord_3 = _homeService.GetWorkTimeRecordsViewModel(userId);//整理前端需要用到的資料傳回
            //如果說今天以內有資料 就會顯現今天的打卡資料 如果沒有今天的資料會將資料庫清除
            ViewBag.worktime = workTimeRecord_3;
           
            //得到最近活動的資料 今日起7天內
            var RecentActivities = _homeService.GetRecentActivities(today, sevenDaysLater);
            ViewBag.Rec_Event = RecentActivities;
            return View();

        }

        public IActionResult OnDuty()
        {
            string userIdStr = Request.Cookies["member_id"];
            int.TryParse(userIdStr, out int userId);
            //得到userId後 傳入打卡上班的函式 完成打卡動作
            _homeService.ClockInToWork(userId);

            return RedirectToAction("Index","Home");
        }

        public IActionResult GetOff()
        {
            string userIdStr = Request.Cookies["member_id"];
            int.TryParse(userIdStr, out int userId);
            //得到userId後 傳入打卡下班的函式 完成打卡動作
            _homeService.ClockOut(userId);

            return RedirectToAction("Index", "Home");
        }



        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}