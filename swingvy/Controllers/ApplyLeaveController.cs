using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using swingvy.Models;

namespace swingvy.Controllers
{
    public class ApplyLeaveController : Controller
    {
        private readonly swingvyContext _swingvyContext;
        public ApplyLeaveController(swingvyContext context)
        {
            _swingvyContext = context;
        }
        public IActionResult Index()
        {
            string? member_id = Request.Cookies["member_id"];
            int.TryParse(member_id, out int memberId);
            var query = from leaveOrder in _swingvyContext.leaveOrder
                        join md1 in _swingvyContext.memberData on leaveOrder.member_id equals md1.member_id
                        join md2 in _swingvyContext.memberData on md1.head equals md2.member_id
                        where leaveOrder.member_id == memberId
                        select new
                        {
                            type =leaveOrder.type,
                            startTime = leaveOrder.startTime,
                            endTime =leaveOrder.endTime,
                            state = leaveOrder.state,
                            head_name = md2.name
                        };
            var resultList = query.ToList();
            ViewBag.apply = resultList;
            return View();
        }
    }
}
