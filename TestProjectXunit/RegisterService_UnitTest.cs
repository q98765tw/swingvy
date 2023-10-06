using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using swingvy.Models;
using swingvy.Repositories;
using swingvy.Services;
using Moq; // 如果您使用 Moq 進行模擬
using swingvy.Enums;
namespace TestProjectXunit
{
    public class RegisterService_UnitTest
    {
        [Fact]
        public async void RegisterUser_Success()
        {
            // 準備測試數據
            string account = "testuser";
            string password = "password123";
            var salt = new byte[64]; // 隨機盐值
            var passwordHash = new byte[64]; // 密碼哈希值

            var memberRepositoryMock = new Mock<IMemberRepository>();
            var calendarRepositoryMock = new Mock<ICalendarRepository>();

            memberRepositoryMock.Setup(repo => repo.AddUser(It.IsAny<member>()));
            calendarRepositoryMock.Setup(repo => repo.AddCalendar(It.IsAny<calendar>()));
            // 如果有其他需要模擬的 IMemberRepository 方法，也可以進行模擬

            var registerService = new RegisterService(memberRepositoryMock.Object, calendarRepositoryMock.Object);

            // 執行註冊
            Task<bool> resultTask = registerService.RegisterUser(account, password);

            // 等待註冊完成
            bool result = await resultTask;

            // 驗證註冊是否成功
            Assert.True(result);

            // 驗證相關方法是否被呼叫
            memberRepositoryMock.Verify(repo => repo.AddUser(It.IsAny<member>()), Times.Once);
            calendarRepositoryMock.Verify(repo => repo.AddCalendar(It.IsAny<calendar>()), Times.Once);
        }
    }
}
