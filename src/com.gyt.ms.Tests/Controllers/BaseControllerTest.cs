﻿using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using com.gyt.ms.Controllers;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Zer.Entities.User;
using Zer.Framework.Extensions;
using Zer.Framework.Mvc;
using Zer.Services.Users;
using Zer.Services.Users.Dto;

namespace com.gyt.ms.Tests.Controllers
{
    [TestFixture]
    public class BaseControllerTest : ControllerTestBase
    {
        [Test]
        [Category("BaseController")]
        [Description("正常获取Session，返回期望值")]
        public void TestForGetCurrentUser_Normal_ReturnCorrectResult()
        {
            using (var userController = new UserController(MockUserInfoServiceForTest()))
            {
                // Arrange
                var expected = new UserInfoDto()
                {
                    DisplayName = "Paul",
                    UserName = "Paulyang",
                    State = UserState.Active
                };

                var mockContext = new Mock<ControllerContext>();

                mockContext.SetupSet(x => x.HttpContext.Session["UserInfo"] = expected);
                mockContext.Setup(x => x.HttpContext.Session["UserInfo"]).Returns(expected);

                userController.ControllerContext = mockContext.Object;

                userController.Login("paulyang", "1234567");

                // Act
                var actual = userController.CurrentUser;

                // Assert
                actual.ShouldBeEquivalentTo(expected);
            }
        }

        [Test]
        [Category("BaseController")]
        [Description("正常获取Session，返回期望值")]
        public void TestForGetCurrentUser_NoSession_ReturnNull()
        {
            Assert.Fail();
        }

        [Test]
        [Category("BaseController")]
        [Description("")]
        public void TestForSuccess_NoMessage_ReturnExpectedJsonResult()
        {
            Assert.Fail();
        }

        [Test]
        [Category("BaseController")]
        [Description("")]
        public void TestForSuccess_JustMessage_ReturnExpectedJsonResult()
        {
            Assert.Fail();
        }

        [Test]
        [Category("BaseController")]
        [Description("")]
        public void TestForSuccess_FullParameters_ReturnExpectedJsonResult()
        {
            Assert.Fail();
        }

        [Test]
        [Category("BaseController")]
        [Description("")]
        public void TestForFail_Normal_ReturnExpectedJsonResult()
        {
            Assert.Fail();
        }

        private IUserInfoService MockUserInfoServiceForTest()
        {
            var mock = new Mock<IUserInfoService>();
            mock.Setup(x => x.GetByUserName("Paulyang"))
                .Returns(() => new UserInfoDto
                                   {
                                       DisplayName = "Paul",
                                       UserName = "Paulyang",
                                       State = UserState.Active
                                   });
            mock.Setup(x => x.VerifyUserNameAndPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => LoginStatus.Success);

            return mock.Object;
        }
    }
}
