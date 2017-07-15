using System.Web.Mvc;
using com.gyt.ms.Controllers;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Zer.Framework.Extensions;
using Zer.Framework.Mvc;
using Zer.Services.Users;
using Zer.Services.Users.Dto;

namespace com.gyt.ms.Tests.Controllers.User
{
    [TestFixture]
    public class UserFrozonTest : ControllerTestBase
    {
        [Test]
        [Category("User.Frozon")]
        public void TestForFrozen_NormalFlow_ReturnExpectedJsonResult()
        {
            using (var ctrl = new UserController(MockUserInfoServiceForTest()))
            {
                var userId = 1;
                var msg = "";
                var data = string.Empty;
                var expected = Json(new { C = ResultCode.Success.ToInt(), msg, data }, JsonRequestBehavior.AllowGet);

                var actual = ctrl.Frozon(userId);
                
                actual.ShouldBeEquivalentTo(expected);
            }
        }

        public static IUserInfoService MockUserInfoServiceForTest()
        {
            var mock=new Mock<IUserInfoService>();
            mock.Setup(x => x.LetUserFrozen(1)).Returns(() => FrozenResult.Success);

            return mock.Object;
        }
    }
}
