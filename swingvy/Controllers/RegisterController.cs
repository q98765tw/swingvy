using Microsoft.AspNetCore.Mvc;
using swingvy.Enums;
using swingvy.Models;
using swingvy.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace swingvy.Controllers
{
    public class RegisterController : Controller
    {
        
        private readonly RegisterService _registerService;
        private readonly MemberRepository _memberRepository;
        private readonly MemberDataRepository _memberDataRepository;
        private readonly WorktimeRepository _worktimeRepository;
        public RegisterController(RegisterService registerService, MemberRepository memberRepository, MemberDataRepository memberDataRepository, WorktimeRepository worktimeRepository)
        {
            _registerService = registerService;
            _memberRepository = memberRepository;
            _memberDataRepository = memberDataRepository;
            _worktimeRepository = worktimeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(int type,int position, string account, string password)
        {
            var existingUser = _memberRepository.GetUserByAccount(account);
            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "用户名已存在";
                ModelState.Clear();
                return View("Index");
            }
            // 使用Service和Repository來處理業務邏輯和數據存取
            _registerService.RegisterUser(account, password);

            //判斷主管員工，並自動登入
            var user = _memberRepository.GetUserByAccountPassword(account, password);
            if (user == null) {
                ViewBag.ErrorMessage = "資料庫存取失敗";
                return View("Index"); 
            }
            else 
            {
                if (position == (int)Position.Manager)
                {
                    var newUserData = new memberData
                    {
                        member_id = user.member_id,
                        name = "請填寫姓名",
                        email = "請填寫信箱",
                        phone = "請填寫電話",
                        type = (Department)type,
                        position = Position.Manager,
                        head = user.member_id,
                        img_url = "~/img/avatar24-01.png"
                    };
                    _memberDataRepository.AddMemberData(newUserData);
                    _memberDataRepository.save();
                    Response.Cookies.Append("member_head", user.member_id.ToString());
                }
                else {
                    var head = _memberDataRepository.FindHead(type,position);
                    var newUserData = new memberData
                    {
                        member_id = user.member_id,
                        name = "請填寫姓名",
                        email = "請填寫信箱",
                        phone = "請填寫電話",
                        type = (Department)type,
                        position = Position.Employee,
                        head = head!.head,
                        img_url = "~/img/avatar24-01.png"
                    };
                    _memberDataRepository.AddMemberData(newUserData);
                    _memberDataRepository.save();
                    Response.Cookies.Append("member_head", head.head.ToString());
                }
                var workTime = new worktime
                {
                    member_id = user.member_id,
                };
                
                _worktimeRepository.AddWorkTime(workTime);
                _worktimeRepository.save();
                Response.Cookies.Append("member_id", user.member_id.ToString());
                Response.Cookies.Append("member_type", type.ToString());
                Response.Cookies.Append("member_position", position.ToString());
               
            }
           
            // 重定向到注册成功后的页面
            return RedirectToAction("Index", "MemberCenter");
        }
    }
}
