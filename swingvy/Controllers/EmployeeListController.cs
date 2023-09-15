using Microsoft.AspNetCore.Mvc;
using swingvy.Models;

namespace swingvy.Controllers
{
    public class EmployeeListController : Controller
    {
        private readonly swingvyContext _swingvyContext;
        public EmployeeListController(swingvyContext context)
        {
            _swingvyContext = context;
        }
        public IActionResult Index()
        {
            var Bag = from w in _swingvyContext.memberData
                      select new
                      {
                          Name = w.name,
                          Depart = w.type,
                          Ption = w.position,
                          Mail = w.email,
                          Phone = w.phone
                      };
            ViewBag.Emp_List = Bag.ToList();
            return View();
        }
    }
}
