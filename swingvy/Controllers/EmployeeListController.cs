using Microsoft.AspNetCore.Mvc;
using swingvy.Enums;
using swingvy.Models;
using swingvy.Services;
using swingvy.Repositories;

namespace swingvy.Controllers
{
    public class EmployeeListController : Controller
    {
        private readonly EmployeeListRepository _EmployeeListRepository;
        public EmployeeListController(EmployeeListRepository context)
        {
            _EmployeeListRepository = context;
        }
        public IActionResult Index()
        {
            var Bag = _EmployeeListRepository.GetEmployeeData();
            ViewBag.Emp_List = Bag;
            return View();
        }
    }
}
