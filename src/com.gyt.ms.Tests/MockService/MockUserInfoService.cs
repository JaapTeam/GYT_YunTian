using Moq;
using Zer.Entities.User;
using Zer.Services.Users;
using Zer.Services.Users.Dto;

namespace com.gyt.ms.Tests.MockService
{
    public class MockUserInfoService : MockRepository<IUserInfoService>
    {
        protected override void SetMock()
        {
            Mock.Setup(x => x.GetByUserName("administrator"))
                .Returns(() => new UserInfoDto
                {
                    DisplayName = "Admin",
                    UserName = "administrator",
                    State = UserState.Active
                });

            Mock.Setup(x => x.VerifyUserNameAndPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => LoginStatus.IncorrectPassword);
        }
    }
}