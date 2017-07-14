using System;
using System.Web;
using Moq;
using Zer.Entities.User;
using Zer.Services.Users;
using Zer.Services.Users.Dto;

namespace com.gyt.ms.Tests
{
    public class MockRepository : IMockRepository
    {
        public virtual HttpContext GetMockHttpContext()
        {
            throw new NotImplementedException();
        }

        public virtual IUserInfoService GetMockUserInfoService()
        {
            var mock = new Mock<IUserInfoService>();

            mock.Setup(x => x.GetByUserName("administrator"))
                .Returns(() => new UserInfoDto
                {
                    DisplayName = "Admin",
                    UserName = "administrator",
                    State = UserState.Active
                });

            mock.Setup(x => x.VerifyUserNameAndPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => LoginStatus.IncorrectPassword);

            return mock.Object;
        }
    }
}