using Microsoft.AspNetCore.Mvc;
using swingvy.Models;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace swingvy.Controllers
{
    public class RegisterController : Controller
    {
        private readonly swingvyContext _swingvyContext;

        public RegisterController(swingvyContext context)
        {
            _swingvyContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string account, string password)
        {
            // 检查用户名是否已存在
            var existingUser = _swingvyContext.member.FirstOrDefault(m => m.account == account);
            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "用户名已存在";
                ModelState.Clear();
                return View("Index");
            }

            // 生成随机盐值
            byte[] salt = GenerateSalt();

            // 计算密码哈希值
            byte[] passwordHash = ComputeHash(password, salt);

            // 创建新用户并存储到数据库
            var newUser = new member
            {
                account = account,
                password = password,
                PasswordHash = passwordHash,
                Salt = salt
            };
            _swingvyContext.member.Add(newUser);
            _swingvyContext.SaveChanges();
            // 注册成功，可以执行其他操作，如登录用户等
            var user = _swingvyContext.member.FirstOrDefault(m => m.account == account && m.password == password);
            if (user != null)
            {
                var newUserData = new memberData
                {
                    member_id = user.member_id,
                    name= user.member_id.ToString(),
                    email = user.member_id.ToString(),
                    phone= user.member_id.ToString(),
                    type = 2,
                    position = 0,
                    head = 1
                };
                var workTime = new worktime
                {
                    member_id = user.member_id,
                };
                _swingvyContext.memberData.Add(newUserData);
                _swingvyContext.worktime.Add(workTime);
                _swingvyContext.SaveChanges();

                Response.Cookies.Append("member_id", user.member_id.ToString());
                Response.Cookies.Append("member_type",2.ToString());
                Response.Cookies.Append("member_position", 0.ToString());
                Response.Cookies.Append("member_head", 1.ToString());
            }
           
            // 重定向到注册成功后的页面
            return RedirectToAction("Index", "EmployeeList");
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[64]; // 64字节的盐值
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private byte[] ComputeHash(string password, byte[] salt)
        {
            using (var hmac = new HMACSHA512(salt))
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                return hmac.ComputeHash(passwordBytes);
            }
        }
    }
}
