﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly CalendarRepository _calendarRepository;
        public CheckLeavePersonController(LeaveOrderRepository leaveOrderRepository, CalendarRepository calendarRepository)
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
                var approve = _leaveOrderRepository.GetLeaveOrderById(leaveOrder_id);
                if (approve != null) { 
                    approve.state = LeaveState.Approve;
                }
                var calendar = new calendar
                {
                    member_id = member_id,
                    name= name,
                    startTime = startTime,
                    endTime = endTime,
                };
                await _calendarRepository.AddCalendar(calendar);
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
                var reject = _leaveOrderRepository.GetLeaveOrderById(leaveOrder_id);
                if (reject != null)
                {
                    reject.state = LeaveState.Reject;
                }
                await _leaveOrderRepository.SaveChange();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"新增時發生錯誤: {ex.Message}");
            }
        }

    }
}
