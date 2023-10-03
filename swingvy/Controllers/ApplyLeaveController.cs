using Microsoft.AspNetCore.Mvc;
using swingvy.Models;
using swingvy.Enums;
using swingvy.Repositories;
using swingvy.Services;

namespace swingvy.Controllers
{
    public class ApplyLeaveController : Controller
    {
        private readonly swingvyContext _swingvyContext;
        //private readonly LoginService _loginService;
        private readonly LeaveOrderRepository _leaveOrderRepository;
        public ApplyLeaveController(swingvyContext context, LeaveOrderRepository leaveOrderRepository)
        {
            _swingvyContext = context;
            _leaveOrderRepository = leaveOrderRepository;
        }
        public IActionResult Index()
        {
            string? member_id = Request.Cookies["member_id"];
            int.TryParse(member_id, out int memberId);
            var resultList = _leaveOrderRepository.GetLeaveOrderData(memberId);
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
                await _leaveOrderRepository.AddLeaveOrder(leaveOrder);
                return Ok();
            }
            catch 
            {
                return StatusCode(500);
            }
        }

    }
}
