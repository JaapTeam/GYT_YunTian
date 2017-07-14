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
    public class UserController_Login_Test : ControllerTestBase
    {
        [Test]
        [Category("User.Login")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestForLogin_UnsafeInput_ThrowArgumentException()
        {
            using (var control = new UserController(MockUserInfoServiceForTest()))
            {
                control.Login("--x!=", "'1=1-");
            }
        }


        [Test]
        [Category("User.Login")]
        [TestCase("admin", "qwerty22", "Error", Description = "密码不正确，跳转到错误页")]
        [TestCase("adminuser1", "qwerty22", "Error", Description = "用户状态被冻结，跳转到错误页")]
        [TestCase("adminuser2", "qwerty22", "Error", Description = "用户名不存在，跳转到错误页")]
        [TestCase("correctusername", "1234567", "Success", Description = "用户名与密码正确，登录成功")]
        public void TestForLogin_NormalFlow_ReturnExpectedView(string userName, string password, string expectedViewName)
        {
            using (var control = new UserController(MockUserInfoServiceForTest()))
            {
                var actual = control.Login(userName,password);
                actual.Should().BeViewResult().WithViewName(expectedViewName);
            }
        }

        public static IUserInfoService MockUserInfoServiceForTest()
        {
            var mock = new Mock<IUserInfoService>();
            mock.Setup(x => x.VerifyUserNameAndPassword("admin", "qwerty22"))
                .Returns(() => LoginStatus.IncorrectPassword);

            mock.Setup(x => x.VerifyUserNameAndPassword("adminuser1", "qwerty22"))
                .Returns(() => LoginStatus.UserFrozen);

            mock.Setup(x => x.VerifyUserNameAndPassword("adminuser2", "qwerty22"))
                .Returns(() => LoginStatus.UserNameNotExists);

            mock.Setup(x => x.VerifyUserNameAndPassword("correctusername", "1234567"))
                .Returns(() => LoginStatus.Success);
            return mock.Object;
        }
    }
}