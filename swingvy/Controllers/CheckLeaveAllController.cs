using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using swingvy.Models;
using swingvy.Enums;
using swingvy.Repositories;
using swingvy.Services;

namespace swingvy.Controllers
{
    public class CheckLeaveAllController : Controller
    {
        private readonly LeaveOrderRepository _leaveOrderRepository;
        public CheckLeaveAllController(LeaveOrderRepository leaveOrderRepository)
        {
            _leaveOrderRepository = leaveOrderRepository;
        }
        
        public IActionResult Index()
        {
            #region 判斷是否是主管
            string? member_position = Request.Cookies["member_position"];
            if (member_position == ((int)Position.Employee).ToString()) { 
                return RedirectToAction("Privacy"); 
            }
            #endregion
            string? member_type = Request.Cookies["member_type"];
            int.TryParse(member_type, out int memberType);
            var resultList = _leaveOrderRepository.GetLeaveOrderAll(memberType);
            ViewBag.leaveList = resultList;
            ViewBag.memberType = (Department)memberType;
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
