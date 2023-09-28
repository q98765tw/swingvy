using Microsoft.AspNetCore.Mvc;
using swingvy.Models;
using System.Security.Cryptography;
using System.Text;
using swingvy.Enums;
using static swingvy.Models.swingvyContext;

namespace swingvy.Controllers
{
    public class LoginController : Controller
    {
        //private readonly swingvyContext _swingvyContext;
        private readonly ISwingvyContext _swingvyContext;
        public LoginController(ISwingvyContext context)
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
            var user = _swingvyContext.member.FirstOrDefault(m => m.account == account);
            if (user != null)
            {
                //驗證hash
                bool isPasswordCorrect = VerifyPasswordHash(password, user.PasswordHash, user.Salt);

                if (isPasswordCorrect)
                {
                    // 登入成功
                    var id = user.member_id;
                    var data = _swingvyContext.memberData.FirstOrDefault(m => m.member_id == id);
                    if (data != null)
                    {
                        Position pos = (Position)data.position;
                        Department typ = (Department)data.type;
                        int positionValue = (int)pos;
                        int typeValue = (int)typ;
                        Response.Cookies.Append("member_id", id.ToString());
                        Response.Cookies.Append("member_type", typeValue.ToString());
                        Response.Cookies.Append("member_position", positionValue.ToString());
                        Response.Cookies.Append("member_head", data.head.ToString());
                    }
                    // 重定向到登录成功后的页面
                    
                }
                return RedirectToAction("Index", "Home");
            }

            // 登入失败
            ViewBag.ErrorMessage = "帐号或密码错误";
            ModelState.Clear();
            return RedirectToAction("Index");
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
