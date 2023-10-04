using Microsoft.AspNetCore.Mvc;
using swingvy.Models;
using System;
using swingvy.Enums;
using swingvy.Repositories;
using swingvy.Services;

namespace swingvy.Controllers
{
    public class MemberCenterController : Controller
    {
        private readonly MemberCenterRepository _memberCenterRepository;
        private readonly MemberCenterService _memberCenterService;
        public MemberCenterController(MemberCenterRepository context, MemberCenterService context_2)
        {
            _memberCenterRepository = context;
            _memberCenterService = context_2;
        }

        public IActionResult Index()
        {
            string userIdStr = Request.Cookies["member_id"];
            int.TryParse(userIdStr, out int userId);

            //抓會員資料傳回ViewBag中
            var Bag = _memberCenterRepository.GetMemberData(userId);
            ViewBag.Ifo = Bag;
            return View();
        }

        [HttpPost]
        public ActionResult ChangeIfo(string GetName, string GetMail, string GetPhone, string GetDepa, string GetPtion)
        {
            string userIdStr = Request.Cookies["member_id"];
            int.TryParse(userIdStr, out int userId);
            //得到職位與部門的資料庫代碼
            int DepaNum = _memberCenterService.GetDepartmentNumber(GetDepa);
            int PtionNum = _memberCenterService.GetPositionNumber(GetPtion);

            //資料寫回到會員的資料庫
            _memberCenterRepository.UpdateMemberInformation(userId,GetName, GetMail, GetPhone, DepaNum, PtionNum);

            return RedirectToAction("Index", "MemberCenter");
        }




        [HttpPost]
        public async Task<IActionResult> UploadProfilePicture(string cropped_image)
        {
            /*var userId = 1;*/
            string userIdStr = Request.Cookies["member_id"];
            int.TryParse(userIdStr, out int userId);

            // 檢查是否有文件被上傳
            if (string.IsNullOrEmpty(cropped_image))
            {
                TempData["Message"] = "請選擇一個文件上傳。";
                return RedirectToAction("Index", "MemberCenter");
            }

            var fileName = $"{userId}HeadPhoto.jpg";

            bool isUploadSuccessful = await _memberCenterService.UploadProfilePictureAsync(userId, cropped_image);

            if (!isUploadSuccessful)
            {
                TempData["Message"] = "頭像上傳失敗。";
                return RedirectToAction("Index", "MemberCenter");
            }

            bool isProfileUrlUpdated = _memberCenterRepository.UpdateProfilePictureUrl(userId, fileName);

            if (!isProfileUrlUpdated)
            {
                TempData["Message"] = "更新用戶頭像路徑失敗。";
                return RedirectToAction("Index", "MemberCenter");
            }

            TempData["Message"] = "頭像上傳成功並更新用戶頭像路徑。";
            return RedirectToAction("Index", "MemberCenter");
        }


    }
}
