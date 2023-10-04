using swingvy.Enums;
using swingvy.Models;

namespace swingvy.Repositories
{
    public class MemberCenterRepository
    {
        private readonly swingvyContext _swingvyContext;

        public MemberCenterRepository(swingvyContext swingvyContext)
        {
            _swingvyContext = swingvyContext;
        }
        public object GetMemberData(int userId)
        {
#pragma warning disable CS8603 // 可能有 Null 參考傳回。
            return (from a in _swingvyContext.memberData
                    where a.member_id == userId
                    select new
                    {
                        Name = a.name,
                        Mail = a.email,
                        Phone = a.phone,
                        Depart = a.type,
                        Position = a.position,
                        Photo = a.img_url
                    }).FirstOrDefault();
#pragma warning restore CS8603 // 可能有 Null 參考傳回。
        }

        //public int GetDepartmentNumber(string department)
        //{
        //    switch (department)
        //    {
        //        case "UI/UX":
        //            return (int)Department.UIUX;
        //        case "前端":
        //            return (int)Department.Frondend;
        //        case "後端":
        //            return (int)Department.Backend;
        //        default:
        //            return (int)Department.Unknown;
        //    }
        //}

        //public int GetPositionNumber(string position)
        //{
        //    switch (position)
        //    {
        //        case "員工":
        //            return (int)Position.Employee;
        //        case "主管":
        //            return (int)Position.Manager;
        //        default:
        //            return (int)Position.Unknown;
        //    }
        //}

        public void UpdateMemberInformation(int userId, string name, string email, string phone, int departmentNumber, int positionNumber)
        {
            var member = _swingvyContext.memberData.FirstOrDefault(n => n.member_id == userId);

            if (member != null)
            {
                member.name = name;
                member.email = email;
                member.phone = phone;
                member.type = (Enums.Department)departmentNumber;
                member.position = (Enums.Position)positionNumber;

                _swingvyContext.SaveChanges();
            }
        }

        //public async Task<bool> UploadProfilePictureAsync(int userId, string croppedImage)
        //{
        //    try
        //    {
        //        var base64Data = croppedImage.Substring(croppedImage.IndexOf(',') + 1);
        //        var imageBytes = Convert.FromBase64String(base64Data);
        //        var fileName = $"{userId}HeadPhoto.jpg";
        //        var filePath = Path.Combine("wwwroot", "img", fileName);
        //        await File.WriteAllBytesAsync(filePath, imageBytes);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        public bool UpdateProfilePictureUrl(int userId, string fileName)
        {
            try
            {
                var user = _swingvyContext.memberData.FirstOrDefault(m => m.member_id == userId);
                if (user == null)
                    return false;

                user.img_url = $"~/img/{fileName}";
                _swingvyContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
