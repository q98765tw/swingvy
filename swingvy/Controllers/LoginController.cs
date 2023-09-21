using Microsoft.AspNetCore.Mvc;
using swingvy.Models;
using System.Security.Cryptography;
using System.Text;

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
            // 帐号密码验证
            var user = _swingvyContext.member.FirstOrDefault(m => m.account == account);
            if (user != null)
            {
                // 验证密码哈希值
                bool isPasswordCorrect = VerifyPasswordHash(password, user.PasswordHash, user.Salt);

                if (isPasswordCorrect)
                {
                    // 登录成功
                    // 根据需要执行其他登录相关的操作，例如设置认证Cookie等

                    // 将用户帐号存入ViewBag以供后续使用
                    var id = user.member_id;
                    var data = _swingvyContext.memberData.FirstOrDefault(m => m.member_id == id);
                    if (data != null)
                    {
                        Response.Cookies.Append("member_id", id.ToString());
                        Response.Cookies.Append("member_type", data.type.ToString());
                        Response.Cookies.Append("member_position", data.position.ToString());
                        Response.Cookies.Append("member_head", data.head.ToString());
                    }
                    // 重定向到登录成功后的页面
                    return RedirectToAction("Index", "Home");
                }
            }

            // 登录失败
            // 可以执行相应的失败处理逻辑，例如显示错误消息
            ViewBag.ErrorMessage = "帐号或密码错误";
            ModelState.Clear();
            return View("Index");
        }

        // 验证密码哈希值的方法
        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                        return false;
                }
            }
            return true;
        }
    }
}
