using System;
using com.gyt.ms.Controllers;
using FluentAssertions;
using NUnit.Framework;
using Zer.Entities;
using Zer.Framework.Entities;
using Zer.NUnit;
using Zer.Services.Users;
using Zer.Services.Users.Dto;

namespace com.gyt.ms.Tests.Controllers
{
    [TestFixture]
    public class BaseControllerTest : ControllerTestBase
    {
        [Test]
        [Category("BaseController")]
        [ExpectException(typeof(ArgumentException))]
        [Description("输入任意非法字符串，都会抛出异常")]
        public void TestForValidataInputString_inputAnyUnsaveString_ThrowException()
        {
            using (var ctrl = new BaseController())
            {
                var stringList = new[]
                {
                    "admin",
                    "sss\"sss"
                };

                ctrl.ValidataInputString(stringList);
            }
        }

        [Test]
        [Category("BaseController")]
        [Description("输入字符串全部合法，不抛出异常")]
        public void TestForValidataInputString_inputAllAreSave_NoExceptionThrow()
        {
            using (var ctrl = new BaseController())
            {
                var stringList = new[]
                {
                    "admin",
                    "123434534",
                    "sdfsddfsdf"
                };

                try
                {
                    ctrl.ValidataInputString(stringList);
                }
                catch
                {
                    Assert.Fail();
                }
            }
        }

        [Test]
        [Category("BaseController")]
        [Description("正常获取Session，返回期望值")]
        public void TestForGetCurrentUser_Normal_ReturnCorrectResult()
        {
            using (var userController = new UserController(MockService<IUserInfoService, UserInfoDto>.Mock()))
            {
                // Arrange
                var expected = new UserInfoDto()
                {
                    DisplayName = "Paul",
                    UserName = "Paulyang",
                    State = UserState.Active
                };


                userController.ControllerContext = MockSpecific.GetMockControllerContextWithSesionUserInfo(expected);

                // Act
                userController.Login("paulyang", "1234567");
                var actual = userController.CurrentUser;

                // Assert
                actual.ShouldBeEquivalentTo(expected);
            }
        }

        //[Test]
        //[Category("BaseController")]
        //[Description("正常获取Session，返回期望值")]
        //public void TestForGetCurrentUser_NoSession_ReturnNull()
        //{
        //    Assert.Reject();
        //}

        //[Test]
        //[Category("BaseController")]
        //[Description("")]
        //public void TestForSuccess_NoMessage_ReturnExpectedJsonResult()
        //{
        //    Assert.Reject();
        //}

        //[Test]
        //[Category("BaseController")]
        //[Description("")]
        //public void TestForSuccess_JustMessage_ReturnExpectedJsonResult()
        //{
        //    Assert.Reject();
        //}

        //[Test]
        //[Category("BaseController")]
        //[Description("")]
        //public void TestForSuccess_FullParameters_ReturnExpectedJsonResult()
        //{
        //    Assert.Reject();
        //}

        //[Test]
        //[Category("BaseController")]
        //[Description("")]
        //public void TestForFail_Normal_ReturnExpectedJsonResult()
        //{
        //    Assert.Reject();
        //}
    }
}
