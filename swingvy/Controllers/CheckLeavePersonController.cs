using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using swingvy.Models;

namespace swingvy.Controllers
{
    public class CheckLeavePersonController : Controller
    {
        private readonly swingvyContext _swingvyContext;
        public CheckLeavePersonController(swingvyContext context)
        {
            _swingvyContext = context;
        }
        public IActionResult Index(int? leaveOrder_id)
        {
            var query = (from L in _swingvyContext.leaveOrder
                         join md1 in _swingvyContext.memberData on L.member_id equals md1.member_id
                         where L.leaveOrder_id == leaveOrder_id
                         select new
                         {
                             name = md1.name,
                             type = L.type,
                             startTime = L.startTime,
                             endTime = L.endTime,
                             applyTime = L.applyTime,
                             reason = L.reason
                         }).FirstOrDefault();
            ViewBag.person=query;
            return View();
        }
    }
}
