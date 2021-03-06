using Moq;
using Zer.AppServices;
using Zer.Entities;
using Zer.GytDto.Users;


namespace com.gyt.ms.Tests.MockService
{
    public class MockUserInfoService : MockRepository<IUserInfoService, UserInfoDto>
    {
        protected override void SetMock()
        {
            Mock.Setup(x => x.GetByUserName("administrator","123"))
                .Returns(() => new UserInfoDto
                {
                    DisplayName = "Admin",
                    UserName = "administrator",
                    UserState = UserState.Active
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

            Mock.Setup(x => x.GetByUserName("correctusername","123"))
                .Returns(() => new UserInfoDto()
                {
                    DisplayName = "Paul",
                    UserState = UserState.Active,
                    UserName = "correctusername"
                });

            Mock.Setup(x => x.LetUserFrozen(1)).Returns(() => FrozenResult.Success);

            Mock.Setup(x => x.VerifyUserNameAndPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => LoginStatus.IncorrectPassword);
        }
    }
}