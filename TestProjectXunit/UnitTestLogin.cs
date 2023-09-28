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
            // �ǳƼ�����swingvyContext
            var mockContext = new Mock<ISwingvyContext>();

            // ������Ʈw�����Τ�
            var mockUser = new member
            {
                member_id=0,
                account = "testuser",
                PasswordHash = new byte[64], // �]�w�X�A�����ƭ�
                Salt = new byte[128] // �]�w�X�A���Q��
            };
            var mockMemberData = new memberData
            {
                memberData_id=0,
                member_id = 0,
                name = "0", // �]�w�X�A�����ƭ�
                email = "0", // �]�w�X�A���Q��
                phone="0",
                type=Department.UIUX,
                position=Position.Employee,
                head=0,
            };
            // �]�m������member�MmemberData
            mockContext.Setup(c => c.member).Returns(new[] { mockUser }.AsQueryable());
            mockContext.Setup(c => c.memberData).Returns(new[] { mockMemberData }.AsQueryable());

            var controller = new LoginController(mockContext.Object);

            // ����Q���ժ���k
            var result = (RedirectToActionResult)controller.Login("testuser", "password123");

            // ���ҵ��G
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            //Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Home", redirectToActionResult.ControllerName);
        }
        [Fact]
        public void Login_ValidUser_ReturnsRedirectToIndex()
        {
            // �ǳƼ�����swingvyContext
            var mockContext = new Mock<ISwingvyContext>();

            // ������Ʈw�����Τ�
            var mockUser = new member
            {
                member_id = 0,
                account = "testuser",
                PasswordHash = new byte[64], // �]�w�X�A�����ƭ�
                Salt = new byte[128] // �]�w�X�A���Q��
            };
            var mockMemberData = new memberData
            {
                memberData_id = 0,
                member_id = 0,
                name = "0", // �]�w�X�A�����ƭ�
                email = "0", // �]�w�X�A���Q��
                phone = "0",
                type = Department.UIUX,
                position = Position.Employee,
                head = 0,
            };
            // �]�m������member�MmemberData
            mockContext.Setup(c => c.member).Returns(new[] { mockUser }.AsQueryable());
            mockContext.Setup(c => c.memberData).Returns(new[] { mockMemberData }.AsQueryable());

            var controller = new LoginController(mockContext.Object);

            // ����Q���ժ���k
            var result = (RedirectToActionResult)controller.Login("1", "password123");

            // ���ҵ��G
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
           
        }
    }
}