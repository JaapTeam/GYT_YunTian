using System;
using com.gyt.ms.Controllers;
using FluentAssertions;
using FluentAssertions.Mvc;
using NUnit.Framework;
using Zer.AppServices;
using Zer.Entities;
using Zer.GytDto.Users;
using Zer.NUnit;

namespace com.gyt.ms.Tests.Controllers.User
{
    [TestFixture]
    public class UserControllerTest : ControllerTestBase
    {
        #region User.Regist

        [Test]
        [Category("User.Regist")]
        [ExpectException(typeof(ArgumentException))]
        public void TestForRegist_UnsafeInput_ThrowArgumentException()
        {
            using (var ctrl = new UserController(MockService<IUserInfoService,UserInfoDto>.Mock()))
            {
                // Act
                ctrl.Regist("-username=", "1'='1'--");
            }
        }

        [Test]
        [Category("User.Regist")]
        [ExpectException(typeof(ArgumentException))]
        public void TestForRegist_InputIsNullOrEmpty_ThrowArgumentException()
        {
            using (var ctrl = new UserController(MockService<IUserInfoService,UserInfoDto>.Mock()))
            {
                // Act
                ctrl.Regist("", "123456");
                ctrl.Regist("administrator", "");
            }
        }

        [Test]
        [Category("User.Regist")]
        [ExpectException(typeof(ArgumentException))]
        public void TestForRegist_InputLengthShort_ThrowArgumentException()
        {
            using (var ctrl = new UserController(MockService<IUserInfoService,UserInfoDto>.Mock()))
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
            using (var ctrl = new UserController(MockService<IUserInfoService,UserInfoDto>.Mock()))
            {
                // Act
                var actual = ctrl.Regist(username, password);

                object data = string.Empty;

                var expected = JsonHelper.SuccessExpected(data);

                // Assert
                actual.ShouldBeEquivalentTo(expected);
            }
        }

        [Test]
        [Category("User.Regist")]
        [TestCase("administrator", "123456", Description = "用户名密码合法，注册成功")]
        public void TestForRegist_UserNameExists_ReturnExpectedJsonResult(string username, string password)
        {
            using (var ctrl = new UserController(MockService<IUserInfoService,UserInfoDto>.Mock()))
            {
                // Act
                var actual = ctrl.Regist(username, password);

                object data = string.Empty;

                var expected = JsonHelper.SuccessExpected(data);

                // Assert
                actual.ShouldBeEquivalentTo(expected);
            }
        }

        #endregion

        #region User.Login

        [Test]
        [Category("User.Login")]
        [ExpectException(typeof(ArgumentException))]
        public void TestForLogin_UnsafeInput_ThrowArgumentException()
        {
            using (var control = new UserController(MockService<IUserInfoService,UserInfoDto>.Mock()))
            {
                control.Login("--x!=", "'1=1-");
            }
        }

        [Test]
        [Category("User.Login")]
        [TestCase("admin", "qwerty22", "Error", Description = "密码不正确，跳转到错误页")]
        [TestCase("adminuser1", "qwerty22", "Error", Description = "用户状态被冻结，跳转到错误页")]
        [TestCase("adminuser2", "qwerty22", "Error", Description = "用户名不存在，跳转到错误页")]
        //[TestCase("correctusername", "1234567", "Success", Description = "用户名与密码正确，登录成功")]
        public void TestForLogin_NormalFlow_ReturnExpectedView(string userName, string password, string expectedViewName)
        {
            using (var control = new UserController(MockService<IUserInfoService,UserInfoDto>.Mock()))
            {
                var actual = control.Login(userName, password);
                actual.Should().BeViewResult().WithViewName(expectedViewName);
            }
        }

        [Test]
        [Category("User.Login")]
        public void TestForLogin_LoginSuccess_WriteSessionSuccess()
        {
            using (var control = new UserController(MockService<IUserInfoService,UserInfoDto>.Mock()))
            {
                // Arrange
                var expected = new UserInfoDto()
                {
                    DisplayName = "Paul",
                    State = UserState.Active,
                    UserName = "correctusername"
                };

                control.ControllerContext = MockSpecific.GetMockControllerContextWithSesionUserInfo(expected);

                // act
                control.Login("correctusername", "1234567");
                var actual = control.CurrentUser;

                actual.Should().NotBeNull();
                actual.ShouldBeEquivalentTo(expected);
            }
        }

        #endregion

        #region User.Thaw

        [Test]
        [Category("User.Thaw")]
        public void TestForThaw_NormalFlow_ReturnExpectedJsonResult()
        {
            using (var ctrl = new UserController(MockService<IUserInfoService,UserInfoDto>.Mock()))
            {
                object data = string.Empty;

                var expected = JsonHelper.SuccessExpected(data);

                var actval = ctrl.Thaw(1);

                actval.ShouldBeEquivalentTo(expected);
            }
        }

        #endregion

        #region User.ChangePassword

        [Test]
        [Category("User.ChangePassword")]
        [ExpectException(typeof(ArgumentException))]
        public void TestForChangePassword_UnsafeInput_ThrowArgumentException()
        {
            using (var control = new UserController(MockService<IUserInfoService,UserInfoDto>.Mock()))
            {
                control.ChangePasswrod(1, "'1=1-");
            }
        }

        [Test]
        [Category("User.ChangePassword")]
        [ExpectException(typeof(ArgumentException))]
        public void TestForChangePassword_PasswordLengthToShort_ThrowArgumentException()
        {
            using (var control = new UserController(MockService<IUserInfoService,UserInfoDto>.Mock()))
            {
                control.ChangePasswrod(1, "123");
            }
        }

        [Test]
        [Category("User.ChangePassword")]
        public void TestForChangePassword_Success_ReturnExpectedSuccessJsonResult()
        {
            using (var ctrl = new UserController(MockService<IUserInfoService,UserInfoDto>.Mock()))
            {
                object data = string.Empty;

                var expected = JsonHelper.SuccessExpected(data);

                var actval = ctrl.ChangePasswrod(1, "123456");

                actval.ShouldBeEquivalentTo(expected);
            }
        }

        [Test]
        [Category("User.ChangePassword")]
        public void TestForChangePassword_SameAsOldPassword_ReturnExpectedFialJsonResult()
        {
            using (var ctrl = new UserController(MockService<IUserInfoService,UserInfoDto>.Mock()))
            {
                var expected = JsonHelper.FailExpected();

                var actval = ctrl.ChangePasswrod(1, "12345678");

                actval.ShouldBeEquivalentTo(expected);
            }
        }


        #endregion

        #region User.Frozon

        [Test]
        [Category("User.Frozon")]
        public void TestForFrozen_NormalFlow_ReturnExpectedJsonResult()
        {
            using (var ctrl = new UserController(MockService<IUserInfoService,UserInfoDto>.Mock()))
            {
                var userId = 1;
                var expected = JsonHelper.SuccessExpected();

                var actual = ctrl.Frozon(userId);

                actual.ShouldBeEquivalentTo(expected);
            }
        }

        #endregion
    }
}