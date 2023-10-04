using swingvy.Enums;
using swingvy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectXunit
{
    public class MemberCenterService_UnitTest
    {
        [Fact]
        public void GetDepartmentNumber_ShouldReturnUIUX()
        {
            // Arrange
            var service = new MemberCenterService();

            // Act
            var result = service.GetDepartmentNumber("UI/UX");

            // Assert
            Assert.Equal((int)Department.UIUX, result);
        }

        [Fact]
        public void GetDepartmentNumber_ShouldReturnFrontend()
        {
            // Arrange
            var service = new MemberCenterService();

            // Act
            var result = service.GetDepartmentNumber("前端");

            // Assert
            Assert.Equal((int)Department.Frondend, result);
        }

        [Fact]
        public void GetDepartmentNumber_ShouldReturnBackend()
        {
            // Arrange
            var service = new MemberCenterService();

            // Act
            var result = service.GetDepartmentNumber("後端");

            // Assert
            Assert.Equal((int)Department.Backend, result);
        }

        [Fact]
        public void GetDepartmentNumber_ShouldReturnUnknown()
        {
            // Arrange
            var service = new MemberCenterService();

            // Act
            var result = service.GetDepartmentNumber("Unknown");

            // Assert
            Assert.Equal((int)Department.Unknown, result);
        }

        [Fact]
        public void GetPositionNumber_ShouldReturnEmployee()
        {
            // Arrange
            var service = new MemberCenterService();

            // Act
            var result = service.GetPositionNumber("員工");

            // Assert
            Assert.Equal((int)Position.Employee, result);
        }

        [Fact]
        public void GetPositionNumber_ShouldReturnManager()
        {
            // Arrange
            var service = new MemberCenterService();

            // Act
            var result = service.GetPositionNumber("主管");

            // Assert
            Assert.Equal((int)Position.Manager, result);
        }

        [Fact]
        public void GetPositionNumber_ShouldReturnUnknown()
        {
            // Arrange
            var service = new MemberCenterService();

            // Act
            var result = service.GetPositionNumber("Unknown");

            // Assert
            Assert.Equal((int)Position.Unknown, result);
        }



        [Fact]
        public async Task UploadProfilePictureAsync_ShouldReturnFalseOnException()
        {
            // Arrange
            var service = new MemberCenterService();

            // Act
            // Passing invalid base64 image data to force an exception
            var result = await service.UploadProfilePictureAsync(1, "invalid_base64_image_data");

            // Assert
            Assert.False(result);
        }
    }
}
