using Microsoft.AspNetCore.Mvc;
using swingvy.Models;
using System.Security.Cryptography;
using System.Text;
using swingvy.Enums;
using static swingvy.Models.swingvyContext;
using swingvy.Repositories;
using swingvy.Services;

namespace swingvy.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;
        private readonly MemberRepository _memberRepository;
        private readonly MemberDataRepository _memberDataRepository;
        private readonly WorktimeRepository _worktimeRepository;
        //private readonly ISwingvyContext _swingvyContext;
        public LoginController(LoginService loginService, MemberRepository memberRepository, MemberDataRepository memberDataRepository, WorktimeRepository worktimeRepository)
        {
            _loginService = loginService;
            _memberRepository = memberRepository;
            _memberDataRepository = memberDataRepository;
            _worktimeRepository = worktimeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string account, string password)
        {
            var user = _memberRepository.GetUserByAccount(account);
            if (user != null)
            {
                //驗證hash
                bool isPasswordCorrect = _loginService.VerifyPasswordHash(password, user.PasswordHash, user.Salt);

                if (isPasswordCorrect)
                {
                    // 登入成功
                    var id = user.member_id;
                    var data = _memberDataRepository.GetUserById(id);
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
                    return RedirectToAction("Index", "Home");
                }
                else {
                    // 登入失败
                    ViewBag.ErrorMessage = "帐号或密码错误";
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
            }
            else {
                // 登入失败
                ViewBag.ErrorMessage = "沒有帐号";
                ModelState.Clear();
                return RedirectToAction("Index");
            }
        }
    }
}
