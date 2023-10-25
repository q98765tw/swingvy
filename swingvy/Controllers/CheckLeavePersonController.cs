using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using swingvy.Enums;
using swingvy.Models;
using swingvy.Repositories;
using swingvy.Services;

namespace swingvy.Controllers
{
    public class CheckLeavePersonController : Controller
    {
        private readonly LeaveOrderRepository _leaveOrderRepository;
        private readonly ICalendarRepository _calendarRepository;
        public CheckLeavePersonController(LeaveOrderRepository leaveOrderRepository, ICalendarRepository calendarRepository)
        {
            _leaveOrderRepository = leaveOrderRepository;
            _calendarRepository = calendarRepository;
        }
        public IActionResult Index(int leaveOrder_id)
        {
            var result = _leaveOrderRepository.GetLeaveOrderPerson(leaveOrder_id);
            ViewBag.person=result;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ApproveLeave(int leaveOrder_id,int member_id,string name, DateTime? startTime, DateTime? endTime)
        {
            try
            {
                ChangeState(leaveOrder_id, LeaveState.Approve);
                await CreateCalendar(member_id, name, startTime, endTime);
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
                ChangeState(leaveOrder_id, LeaveState.Reject);
                await _leaveOrderRepository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"新增時發生錯誤: {ex.Message}");
            }
        }

        private async Task CreateCalendar(int member_id, string name, DateTime? startTime, DateTime? endTime)
        {
            var calendar = new calendar
            {
                member_id = member_id,
                name = name,
                startTime = startTime,
                endTime = endTime,
            };
            _calendarRepository.AddCalendar(calendar);
            await _calendarRepository.Save();
        }

        private void ChangeState(int leaveOrder_id, LeaveState state) 
        {
            var LeaveOrder = _leaveOrderRepository.GetLeaveOrderById(leaveOrder_id);
            if (LeaveOrder != null)
            {
                LeaveOrder.state = state;
            }
        }
    }
}
