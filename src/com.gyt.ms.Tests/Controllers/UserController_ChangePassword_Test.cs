using System;
using System.Web.Mvc;
using com.gyt.ms.Controllers;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Zer.Framework.Extensions;
using Zer.Framework.Mvc;
using Zer.NUnit;
using Zer.Services.Users;
using Zer.Services.Users.Dto;

namespace com.gyt.ms.Tests.Controllers
{
    [TestFixture]
    public class UserControllerChangePasswordTest:ControllerTestBase
    {
        [Test]
        [Category("User.ChangePassword")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestForChangePassword_UnsafeInput_ThrowArgumentException()
        {
            using (var control = new UserController(MockUserInfoServiceForTest()))
            {
                control.ChangePasswrod(1, "'1=1-");
            }
        }

        [Test]
        [Category("User.ChangePassword")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestForChangePassword_PasswordLengthToShort_ThrowArgumentException()
        {
            using (var control = new UserController(MockUserInfoServiceForTest()))
            {
                control.ChangePasswrod(1, "123");
            }
        }

        [Test]
        [Category("User.ChangePassword")]
        public void TestForChangePassword_Success_ReturnExpectedSuccessJsonResult()
        {
            using (var ctrl=new UserController(MockUserInfoServiceForTest()))
            {
                const string msg = "";
                object data = string.Empty;

                var expected = Json(new
                {
                    C = ResultCode.Success.ToInt(),
                    msg,
                    data
                }, JsonRequestBehavior.AllowGet);

                var actval = ctrl.ChangePasswrod(1, "123456");

                actval.ShouldBeEquivalentTo(expected);
            }
        }

        [Test]
        [Category("User.ChangePassword")]
        public void TestForChangePassword_SameAsOldPassword_ReturnExpectedFialJsonResult()
        {
            using (var ctrl = new UserController(MockUserInfoServiceForTest()))
            {
                const string msg = "";
                object data = string.Empty;

                var expected = Json(new
                {
                    C = ResultCode.Fail.ToInt(),
                    msg,
                    data
                }, JsonRequestBehavior.AllowGet);

                var actval = ctrl.ChangePasswrod(1, "12345678");

                actval.ShouldBeEquivalentTo(expected);
            }
        }

        private IUserInfoService MockUserInfoServiceForTest()
        {
            var mock=new Mock<IUserInfoService>();

            mock.Setup(x => x.ChangePassword(1, "123456"))
                .Returns(() => ChangePasswordResult.Success);

            mock.Setup(x => x.ChangePassword(1, "12345678"))
                .Returns(() => ChangePasswordResult.SameAsOldPassword);
            
            return mock.Object;
        }

    }
}
