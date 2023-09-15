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
        [HttpPost]
        public ActionResult Login(string account, string password)
        {
            // 帳號密碼驗證
            var user = _swingvyContext.member.FirstOrDefault(m => m.account == account && m.password == password);
            // 假設這裡使用一個簡單的驗證邏輯
            if (user != null)
            {
                // 登入成功
                // 可以根據需要執行其他登入相關的操作，例如設置認證Cookie等

                // 將使用者帳號存入ViewBag以供後續使用
                var id = user.member_id;
                var data = _swingvyContext.memberData.FirstOrDefault(m => m.member_id == id);
                if (data != null)
                {
                    Response.Cookies.Append("member_id", id.ToString());
                    Response.Cookies.Append("member_type", data.type.ToString());
                    Response.Cookies.Append("member_position", data.position.ToString());
                    Response.Cookies.Append("member_head", data.head.ToString());
                }
                // 重定向到登入成功後的頁面
                return RedirectToAction("Index","Home");
            }
            else
            {
                // 登入失敗
                // 可以執行相應的失敗處理邏輯，例如顯示錯誤訊息
                ViewBag.ErrorMessage = "帳號或密碼錯誤";
                ModelState.Clear();
                return View("Index");
            }
        }
    }
}
