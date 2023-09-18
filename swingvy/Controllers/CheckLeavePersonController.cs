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
                             leaveOrder_id = L.leaveOrder_id,
                             member_id = L.member_id,
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
        [HttpPost]
        public async Task<IActionResult> ApproveLeave(int leaveOrder_id,int member_id,string name, DateTime? startTime, DateTime? endTime)
        {
            try
            {
                var approve = _swingvyContext.leaveOrder.Find(leaveOrder_id);
                if (approve != null) { 
                    approve.state = 1;
                }
                var calendar = new calendar
                {
                    member_id = member_id,
                    name= name,
                    startTime = startTime,
                    endTime = endTime,
                };
                _swingvyContext.calendar.Add(calendar);
                await _swingvyContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"新增時發生錯誤: {ex.Message}");
            }
        }
        public async Task<IActionResult> RejectLeave(int leaveOrder_id, int member_id, string name, DateTime? startTime, DateTime? endTime)
        {
            try
            {
                var reject = _swingvyContext.leaveOrder.Find(leaveOrder_id);
                if (reject != null)
                {
                    reject.state = 2;
                }              
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
