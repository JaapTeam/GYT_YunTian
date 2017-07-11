using System;
using com.gyt.ms.Controllers;
using FluentAssertions.Mvc;
using Moq;
using NUnit.Framework;
using Zer.Services.Users;
using Zer.Services.Users.Dto;
using Zer.NUnit;

namespace com.gyt.ms.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTest : ControllerTestBase
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestForRegist_UnsafeInput_ThrowArgumentException()
        {
            using (var ctrl = new UserController(MockUserInfoService()))
            {
                // Act
                ctrl.Regist("-username=", "1'='1'--");
            }
        }

        [Test]
        [Category("User.Regist")]
        [TestCase("admin", "1234567", "Error", Description = "�û�������С��6,����Errorҳ��")]
        [TestCase("", "1234567", "Error", Description = "�û���Ϊ��,����Errorҳ��")]
        [TestCase("correctusername", "", "Error", Description = "����Ϊ��,����Errorҳ��")]
        [TestCase("correctusername", "123", "Error", Description = "���볤��С��6,����Errorҳ��")]
        [TestCase("sdfsdfsdf", "sdfsdfsdfsdf", "login", Description = "��ȷ���û���������,����loginҳ��")] 
        public void TestForRegist_NormalFlow_ReturnExpectedView(string username,string password,string expectedPageName)
        {
            using (var ctrl = new UserController(MockUserInfoService()))
            {
                // Act
                var actual = ctrl.Regist(username, password);

                //// Assert
                actual.Should().BeViewResult().WithViewName(expectedPageName);
            }
        }

        private static IUserInfoService MockUserInfoService()
        {
            var mockUserInfoService = new Mock<IUserInfoService>();
            mockUserInfoService.Setup(x => x.Regist("admin", "1234567")).Returns(() => RegistResult.UserNameExists);
            mockUserInfoService.Setup(x => x.Regist("correctUserName", "correctUserName"))
                .Returns(() => RegistResult.Success);
            return mockUserInfoService.Object;
        }
    }
}