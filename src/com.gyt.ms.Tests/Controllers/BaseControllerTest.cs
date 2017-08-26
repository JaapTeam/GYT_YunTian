using System;
using com.gyt.ms.Controllers;
using FluentAssertions;
using NUnit.Framework;
using Zer.AppServices;
using Zer.Entities;
using Zer.Framework.Exception;
using Zer.GytDto;
using Zer.GytDto.Users;
using Zer.NUnit;


namespace com.gyt.ms.Tests.Controllers
{
    [TestFixture]
    public class BaseControllerTest : ControllerTestBase
    {
        //[Test]
        //[Category("BaseController")]
        //[Description("输入任意非法字符串，都会抛出异常")]
        //public void TestForValidataInputString_inputAnyUnsaveString_ThrowException()
        //{
        //    using (var ctrl = new BaseController())
        //    {
        //        var stringList = new[]
        //        {
        //            "admin",
        //            "sss\"sss"
        //        };

        //        try
        //        {
        //            ctrl.ValidataInputString(stringList);
        //        }
        //        catch (Exception actual)
        //        {
        //            actual.Should().BeOfType<CustomException>();
        //            actual.Message.Contains("参数含有非法字符！").Should().BeTrue();
        //        }
        //    }
        //}

        //[Test]
        //[Category("BaseController")]
        //[Description("输入任意非法字符串，都会抛出异常")]
        //public void TestForValidataInputString_inputDtoObject_ThrowException()
        //{
        //    using (var ctrl = new BaseController())
        //    {
        //        var dto = new LngAllowanceInfoDto
        //        {
        //            CompanyName = "sss\"sss",
        //            CylinderDefaultId = "0001010001",
        //            CylinderSeconedId = "sdfjl1009\'"
        //        };

        //        try
        //        {
        //            ctrl.ValidataInputString(dto);
        //        }
        //        catch (Exception actual)
        //        {
        //            actual.Should().BeOfType<CustomException>();
        //            actual.Message.Contains("参数含有非法字符！").Should().BeTrue();
        //        }
        //    }
        //}

        //[Test]
        //[Category("BaseController")]
        //[Description("输入字符串全部合法，不抛出异常")]
        //public void TestForValidataInputString_inputAllAreSave_NoExceptionThrow()
        //{
        //    using (var ctrl = new BaseController())
        //    {
        //        var stringList = new[]
        //        {
        //            "admin",
        //            "123434534",
        //            "sdfsddfsdf"
        //        };

        //        try
        //        {
        //            ctrl.ValidataInputString(stringList);
        //        }
        //        catch
        //        {
        //            Assert.Fail();
        //        }
        //    }
        //}

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
                    UserState = UserState.Active
                };


                userController.ControllerContext = MockSpecific.GetMockControllerContextWithSesionUserInfo(expected);

                // Act
                userController.Login("paulyang", "1234567");
                var actual = userController.CurrentUser;

                // Assert
                actual.ShouldBeEquivalentTo(expected);
            }
        }

        [Test]
        public void TestFor_ReplaceUnSafeChar_Success()
        {
            using (var ctrl = new BaseController())
            {
                var userinfoDto = new UserInfoDto()
                {
                    DisplayName = "a-b-c",
                    UserState = UserState.Active,
                    UserId = 1,
                    UserName = "x-y-z"
                };

                var expected = new UserInfoDto()
                {
                    DisplayName = "a_b_c",
                    UserState = UserState.Active,
                    UserId = 1,
                    UserName = "x_y_z"
                };

                var actual = ctrl.ReplaceUnsafeChar(userinfoDto);

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
