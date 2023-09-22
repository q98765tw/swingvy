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
        public ActionResult ChangeIfo(string GetName, string GetMail, string GetPhone, string GetDepa, string GetPtion)
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




        [HttpPost]
        public async Task<IActionResult> UploadProfilePicture(string cropped_image)
        {
            /*var userId = 1;*/
            string userIdStr = Request.Cookies["member_id"];
            int.TryParse(userIdStr, out int userId);

            // 從身份驗證 Cookie 中獲取當前登錄會員的 ID
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // 從 Cookie 中獲取當前登錄會員的 ID
            //var userId = Request.Cookies["YourCookieName"];

            // 檢查是否有文件被上傳
            if (string.IsNullOrEmpty(cropped_image))
            {
                TempData["Message"] = "請選擇一個文件上傳。";
                return RedirectToAction("Index", "MemberCenter");
            }

            // 將 base64 編碼的圖片數據轉換為 byte[]
            var base64Data = cropped_image.Substring(cropped_image.IndexOf(',') + 1);
            var imageBytes = Convert.FromBase64String(base64Data);

            // 將文件保存到某個路徑
            var fileName = userId +"HeadPhoto"+".jpg";  // 裁剪後的圖片將被保存為 .jpg 文件
            var filePath = Path.Combine("wwwroot", "img", fileName);
            await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

            // 更新使用者的頭像路徑
            var user = _swingvyContext.memberData.FirstOrDefault(m => m.member_id == userId);

            if (user == null)
            {
                // 處理 user 為 null 的情況
                TempData["Message"] = "User not found.";
                return RedirectToAction("Index", "MemberCenter");
            }
            user.img_url = "~/img/" + fileName;

            _swingvyContext.SaveChanges();
            TempData["Message"] = "頭像上傳成功。";
            return RedirectToAction("Index", "MemberCenter");
        }


    }
}
