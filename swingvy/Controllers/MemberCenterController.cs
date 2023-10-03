using Microsoft.AspNetCore.Mvc;
using swingvy.Models;
using System;
using swingvy.Enums;
using swingvy.Services;

namespace swingvy.Controllers
{
    public class MemberCenterController : Controller
    {
        private readonly MemberCenterService _memberCenterService;
        public MemberCenterController(MemberCenterService context)
        {
            _memberCenterService = context;
        }

        public IActionResult Index()
        {
            string userIdStr = Request.Cookies["member_id"];
            int.TryParse(userIdStr, out int userId);

            var Bag = _memberCenterService.GetMemberData(userId);
            ViewBag.Ifo = Bag;
            return View();
        }

        [HttpPost]
        public ActionResult ChangeIfo(string GetName, string GetMail, string GetPhone, string GetDepa, string GetPtion)
        {
            string userIdStr = Request.Cookies["member_id"];
            int.TryParse(userIdStr, out int userId);

            int DepaNum = _memberCenterService.GetDepartmentNumber(GetDepa);
            int PtionNum = _memberCenterService.GetPositionNumber(GetPtion);

            _memberCenterService.UpdateMemberInformation(userId,GetName, GetMail, GetPhone, DepaNum, PtionNum);

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

            bool isProfileUrlUpdated = _memberCenterService.UpdateProfilePictureUrl(userId, fileName);

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
