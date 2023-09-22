using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using swingvy.Models;
using swingvy.Enums;

namespace swingvy.Controllers
{
    public class CheckLeaveAllController : Controller
    {
        private readonly swingvyContext _swingvyContext;
        public CheckLeaveAllController(swingvyContext context)
        {
            _swingvyContext = context;
        }
        public IActionResult Index()
        {
            string? member_position = Request.Cookies["member_position"];
            if (member_position == "0") { return RedirectToAction("Privacy", "Home"); }
            string? member_type = Request.Cookies["member_type"];
            int.TryParse(member_type, out int memberType);
            var query = from leaveOrder in _swingvyContext.leaveOrder
                        join md1 in _swingvyContext.memberData on leaveOrder.member_id equals md1.member_id
                        where md1.type == (Department)memberType && leaveOrder.state == LeaveState.Not
                        orderby leaveOrder.startTime ascending
                        select new
                        {
                            leaveOrder_id = leaveOrder.leaveOrder_id,
                            name = md1.name,
                            applyTime = leaveOrder.applyTime,
                            leaveType = leaveOrder.type,
                            startTime = leaveOrder.startTime,
                            endTime = leaveOrder.endTime,
                        };
            var resultList = query.ToList();
            ViewBag.leaveList = resultList;
            ViewBag.memberType = memberType;
            return View();
        }
    }
}
