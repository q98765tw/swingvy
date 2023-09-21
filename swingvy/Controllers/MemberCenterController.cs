using Microsoft.AspNetCore.Mvc;
using swingvy.Models;
using System;

namespace swingvy.Controllers
{
    public class MemberCenterController : Controller
    {
        private readonly swingvyContext _swingvyContext;
        public MemberCenterController(swingvyContext context)
        {
            _swingvyContext = context;
        }

        public IActionResult Index()
        {
            string userIdStr = Request.Cookies["member_id"];
            int.TryParse(userIdStr, out int userId);
            var Bag = from a in _swingvyContext.memberData
                      where a.member_id == userId
                      select new
                      {
                          Name = a.name,
                          Mail = a.email,
                          Phone = a.phone,
                          Depart = a.type,
                          Position = a.position,
                          Photo = a.img_url
                      };
            ViewBag.Ifo = Bag.FirstOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult ChangeIfo(string GetName,string GetMail,string GetPhone,string GetDepa,string GetPtion)
        {
            string userIdStr = Request.Cookies["member_id"];
            int.TryParse(userIdStr, out int userId);

            int DepaNum;
            int PtionNum;
            switch (GetDepa)
            {
                case "UI/UX":
                    DepaNum = 0;
                    break;
                case "前端":
                    DepaNum = 1;
                    break;
                case "後端":
                    DepaNum = 2;
                    break;
                default:
                    DepaNum = 404;
                    break;
            }

            switch (GetPtion)
            {
                case "員工":
                    PtionNum = 0;
                    break;
                case "主管":
                    PtionNum = 1;
                    break;
                default:
                    PtionNum = 404;
                    break;
            }

            var notebook = _swingvyContext.memberData.FirstOrDefault(n => n.member_id == userId);
            if (notebook != null)
            {
                notebook.name = GetName;
                notebook.email = GetMail;
                notebook.phone = GetPhone;
                notebook.type = (Enums.Department)DepaNum;
                notebook.position = (Enums.Position)PtionNum;
                _swingvyContext.SaveChanges();
            };
            return RedirectToAction("Index", "MemberCenter");
        }
    }
}
