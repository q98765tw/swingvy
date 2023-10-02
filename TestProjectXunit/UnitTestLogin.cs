using System.Diagnostics.Metrics;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Moq;
using swingvy.Controllers;
using swingvy.Enums;
using swingvy.Models;
using Xunit;
using static swingvy.Models.swingvyContext;

namespace TestProjectXunit
{
    public class UnitTestLogin
    {
        [Fact]
        public void Login_ValidUser_ReturnsRedirectToHome()
        {
            // 準備模擬的swingvyContext
            var mockContext = new Mock<ISwingvyContext>();

            // 模擬資料庫中的用戶
            var mockUser = new member
            {
                member_id=0,
                account = "testuser",
                PasswordHash = new byte[64], // 設定合適的哈希值
                Salt = new byte[128] // 設定合適的鹽值
            };
            var mockMemberData = new memberData
            {
                memberData_id=0,
                member_id = 0,
                name = "0", // 設定合適的哈希值
                email = "0", // 設定合適的鹽值
                phone="0",
                type=Department.UIUX,
                position=Position.Employee,
                head=0,
            };
            // 設置模擬的member和memberData
            mockContext.Setup(c => c.member).Returns(new[] { mockUser }.AsQueryable());
            mockContext.Setup(c => c.memberData).Returns(new[] { mockMemberData }.AsQueryable());

            var controller = new LoginController(mockContext.Object);

            // 執行被測試的方法
            var result = (RedirectToActionResult)controller.Login("testuser", "password123");

            // 驗證結果
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            //Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Home", redirectToActionResult.ControllerName);
        }
        [Fact]
        public void Login_ValidUser_ReturnsRedirectToIndex()
        {
            // 準備模擬的swingvyContext
            var mockContext = new Mock<ISwingvyContext>();

            // 模擬資料庫中的用戶
            var mockUser = new member
            {
                member_id = 0,
                account = "testuser",
                PasswordHash = new byte[64], // 設定合適的哈希值
                Salt = new byte[128] // 設定合適的鹽值
            };
            var mockMemberData = new memberData
            {
                memberData_id = 0,
                member_id = 0,
                name = "0", // 設定合適的哈希值
                email = "0", // 設定合適的鹽值
                phone = "0",
                type = Department.UIUX,
                position = Position.Employee,
                head = 0,
            };
            // 設置模擬的member和memberData
            mockContext.Setup(c => c.member).Returns(new[] { mockUser }.AsQueryable());
            mockContext.Setup(c => c.memberData).Returns(new[] { mockMemberData }.AsQueryable());

            var controller = new LoginController(mockContext.Object);

            // 執行被測試的方法
            var result = (RedirectToActionResult)controller.Login("1", "password123");

            // 驗證結果
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
           
        }
    }
}