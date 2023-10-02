using Microsoft.AspNetCore.Mvc;
using swingvy.Enums;
using swingvy.Models;
using swingvy.Services;

namespace swingvy.Controllers
{
    public class EmployeeListController : Controller
    {
        private readonly EmployeeListService _EmployeeListService;
        public EmployeeListController(EmployeeListService context)
        {
            _EmployeeListService = context;
        }
        public IActionResult Index()
        {
            var Bag = _EmployeeListService.GetEmployeeData();
            ViewBag.Emp_List = Bag;
            return View();
        }
    }
}
