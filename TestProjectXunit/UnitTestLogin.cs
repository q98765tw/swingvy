
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Moq;
using swingvy.Controllers;
using swingvy.Enums;
using swingvy.Models;
using swingvy.Repositories;
using swingvy.Services;
using Xunit;
using static swingvy.Models.swingvyContext;

namespace TestProjectXunit
{
    
    public class UnitTestLogin
    {
        [Fact]
        public void VerifyPasswordHash_CorrectPassword_ReturnsTrue()
        {
            // Arrange
            string password = "your_password"; // 替换成你的密码
            byte[] salt = GenerateSalt(); // 替换成你的生成盐的方法
            byte[] hash = GeneratePasswordHash(password, salt); // 替换成你的生成哈希的方法

            var loginService = new LoginService();

            // Act
            bool result = loginService.VerifyPasswordHash(password, hash, salt);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyPasswordHash_IncorrectPassword_ReturnsFalse()
        {
            // Arrange
            string correctPassword = "correct_password";
            string incorrectPassword = "incorrect_password";
            byte[] salt = GenerateSalt();
            byte[] hash = GeneratePasswordHash(correctPassword, salt);

            var loginService = new LoginService();

            // Act
            bool result = loginService.VerifyPasswordHash(incorrectPassword, hash, salt);

            // Assert
            Assert.False(result);
        }

        private byte[] GenerateSalt()
        {
            // 实现生成盐的方法，以供测试使用
            byte[] salt = new byte[64]; // 64字节的盐值
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private byte[] GeneratePasswordHash(string password, byte[] salt)
        {
            // 实现生成哈希的方法，以供测试使用
            using (var hmac = new HMACSHA512(salt))
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                return hmac.ComputeHash(passwordBytes);
            }
        }
    }
}