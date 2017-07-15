using System;
using System.Web.Mvc;
using com.gyt.ms.Controllers;
using FluentAssertions;
using NUnit.Framework;
using Zer.Framework.Extensions;
using Zer.Framework.Mvc;
using Zer.NUnit;

namespace com.gyt.ms.Tests.Controllers.User
{
    [TestFixture]
    public class UserControllerTest : ControllerTestBase
    {
        [Test]
        [Category("User.Regist")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestForRegist_UnsafeInput_ThrowArgumentException()
        {
            using (var ctrl = new UserController(MockRepository.GetMockUserInfoService()))
            {
                // Act
                ctrl.Regist("-username=", "1'='1'--");
            }
        }

        [Test]
        [Category("User.Regist")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestForRegist_InputIsNullOrEmpty_ThrowArgumentException()
        {
            using (var ctrl = new UserController(MockRepository.GetMockUserInfoService()))
            {
                // Act
                ctrl.Regist("", "123456");
                ctrl.Regist("administrator", "");
            }
        }

        [Test]
        [Category("User.Regist")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestForRegist_InputLengthShort_ThrowArgumentException()
        {
            using (var ctrl = new UserController(MockRepository.GetMockUserInfoService()))
            {
                // Act
                ctrl.Regist("admin", "123456");
                ctrl.Regist("administrator", "123");
            }
        }

        [Test]
        [Category("User.Regist")]
        [TestCase("correctusername", "123456", Description = "用户名密码合法，注册成功")]
        public void TestForRegist_RegistSuccess_ReturnExpectedJsonResult(string username, string password)
        {
            using (var ctrl = new UserController(MockRepository.GetMockUserInfoService()))
            {
                // Act
                var actual = ctrl.Regist(username, password);

                const string msg = "";
                object data = string.Empty;

                var expected = Json(new
                {
                    C = ResultCode.Success.ToInt(),
                    msg,
                    data
                }, JsonRequestBehavior.AllowGet);

                // Assert
                actual.ShouldBeEquivalentTo(expected);
            }
        }

        [Test]
        [Category("User.Regist")]
        [TestCase("administrator", "123456", Description = "用户名密码合法，注册成功")]
        public void TestForRegist_UserNameExists_ReturnExpectedJsonResult(string username, string password)
        {
            using (var ctrl = new UserController(MockRepository.GetMockUserInfoService()))
            {
                // Act
                var actual = ctrl.Regist(username, password);

                const string msg = "";
                object data = string.Empty;

                var expected = Json(new
                {
                    C = ResultCode.Fail.ToInt(),
                    msg,
                    data
                }, JsonRequestBehavior.AllowGet);

                // Assert
                actual.ShouldBeEquivalentTo(expected);
            }
        }
    }
}