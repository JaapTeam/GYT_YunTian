using Moq;
using Zer.Entities.User;
using Zer.Services.Users;
using Zer.Services.Users.Dto;

namespace com.gyt.ms.Tests.MockService
{
    public class MockUserInfoService : MockRepository<IUserInfoService, UserInfoDto>
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

            Mock.Setup(x => x.ChangePassword(1, "123456"))
                .Returns(() => ChangePasswordResult.Success);

            Mock.Setup(x => x.ChangePassword(1, "12345678"))
                .Returns(() => ChangePasswordResult.SameAsOldPassword);

            Mock.Setup(x => x.LetUserThaw(1))
                .Returns(() => ThawResult.Success);


            Mock.Setup(x => x.VerifyUserNameAndPassword("admin", "qwerty22"))
                .Returns(() => LoginStatus.IncorrectPassword);

            Mock.Setup(x => x.VerifyUserNameAndPassword("adminuser1", "qwerty22"))
                .Returns(() => LoginStatus.UserFrozen);

            Mock.Setup(x => x.VerifyUserNameAndPassword("adminuser2", "qwerty22"))
                .Returns(() => LoginStatus.UserNameNotExists);

            Mock.Setup(x => x.VerifyUserNameAndPassword("correctusername", "1234567"))
                .Returns(() => LoginStatus.Success);

            Mock.Setup(x => x.GetByUserName("correctusername"))
                .Returns(() => new UserInfoDto()
                {
                    DisplayName = "Paul",
                    State = UserState.Active,
                    UserName = "correctusername"
                });

            Mock.Setup(x => x.LetUserFrozen(1)).Returns(() => FrozenResult.Success);

            Mock.Setup(x => x.VerifyUserNameAndPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => LoginStatus.IncorrectPassword);
        }
    }
}