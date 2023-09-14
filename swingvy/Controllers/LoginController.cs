using Microsoft.AspNetCore.Mvc;
using swingvy.Models;

namespace swingvy.Controllers
{
    public class LoginController : Controller
    {
        private readonly swingvyContext _swingvyContext;
        public LoginController(swingvyContext context)
        {
            _swingvyContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
