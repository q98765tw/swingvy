using swingvy.Enums;
using swingvy.Models;
using swingvy.Repositories;
using System.Security.Cryptography;
using System.Text;


namespace swingvy.Services
{
    public class MemberCenterService
    {
        public int GetDepartmentNumber(string department)
        {
            switch (department)
            {
                case "UI/UX":
                    return (int)Department.UIUX;
                case "前端":
                    return (int)Department.Frondend;
                case "後端":
                    return (int)Department.Backend;
                default:
                    return (int)Department.Unknown;
            }
        }

        public int GetPositionNumber(string position)
        {
            switch (position)
            {
                case "員工":
                    return (int)Position.Employee;
                case "主管":
                    return (int)Position.Manager;
                default:
                    return (int)Position.Unknown;
            }
        }

        public async Task<bool> UploadProfilePictureAsync(int userId, string croppedImage)
        {
            try
            {
                // Check if croppedImage is null or empty
                if (string.IsNullOrEmpty(croppedImage))
                { 
                    return false; 
                }

                var base64Data = croppedImage.Substring(croppedImage.IndexOf(',') + 1);
                var imageBytes = Convert.FromBase64String(base64Data);

                // Create the directory if it doesn't exist
                var directoryPath = Path.Combine("wwwroot", "img");
                Directory.CreateDirectory(directoryPath);

                var fileName = $"{userId}HeadPhoto.jpg";
                var filePath = Path.Combine(directoryPath, fileName);

                // Write the image bytes to the file
                await File.WriteAllBytesAsync(filePath, imageBytes);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
