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
    public class UserControllerThawTset:ControllerTestBase
    {
        [Test]
        [Category("User.Thaw")]
        public void TestForThaw_NormalFlow_ReturnExpectedJsonResult()
        {
            using (var ctrl=new UserController(MockUserInfoServiceForTest()) )
            {
                const string msg = "";
                object data = string.Empty;

                var expected = Json(new
                {
                    C = ResultCode.Success.ToInt(),
                    msg,
                    data
                }, JsonRequestBehavior.AllowGet);

                var actval = ctrl.Thaw(1);

                actval.ShouldBeEquivalentTo(expected);
            }
        }

        public static IUserInfoService MockUserInfoServiceForTest()
        {
            var mock=new Mock<IUserInfoService>();
            mock.Setup(x => x.LetUserThaw(1))
                .Returns(() => ThawResult.Success);

            return mock.Object;
        }
    }
}
