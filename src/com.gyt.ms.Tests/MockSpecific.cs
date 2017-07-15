using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using com.gyt.ms.Tests.MockService;
using Moq;
using Zer.Entities.User;
using Zer.Services.Company;
using Zer.Services.Company.Dto;
using Zer.Services.Users;
using Zer.Services.Users.Dto;

namespace com.gyt.ms.Tests
{
    public class MockSpecific
    {
        /// <summary>
        /// mock <see cref="ControllerContext"/>, 给<see cref="HttpSessionStateBase"/> 赋值，并定义返回行为
        /// </summary>
        /// <param name="expected">期望的返回值</param>
        /// <returns><see cref="ControllerContext"/></returns>
        public static ControllerContext GetMockControllerContextWithSesionUserInfo(UserInfoDto expected)
        {
            var mockContext = new Mock<ControllerContext>();

            mockContext.SetupSet(x => x.HttpContext.Session["UserInfo"] = expected);
            mockContext.Setup(x => x.HttpContext.Session["UserInfo"]).Returns(expected);

            return mockContext.Object;
        }
    }
}