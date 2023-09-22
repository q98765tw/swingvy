using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using swingvy.Models;
using System.Reflection;
using swingvy.Enums;
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
                        orderby leaveOrder.startTime ascending
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

        [HttpPost]
        public async Task<IActionResult> ApplyLeave(int leaveType, DateTime? applyTime, DateTime? startTime, DateTime? endTime, string reason)
        {
            try
            {
                string? member_id = Request.Cookies["member_id"];
                string? member_head = Request.Cookies["member_head"];
                int.TryParse(member_id, out int memberId);
                int.TryParse(member_head, out int memberHead);
                var leaveOrder = new leaveOrder
                {
                    member_id = memberId,
                    type = (Enums.LeaveType)leaveType,
                    startTime = startTime,
                    endTime = endTime,
                    applyTime = applyTime,
                    reason = reason,
                    state = LeaveState.Not,
                    head = memberHead,
                };
                _swingvyContext.leaveOrder.Add(leaveOrder);
                await _swingvyContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"新增時發生錯誤: {ex.Message}");
            }
        }

    }
}
