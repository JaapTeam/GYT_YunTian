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

            #region GetByUserName
            mock.Setup(x => x.GetByUserName("administrator"))
                .Returns(() => new UserInfoDto
                {
                    DisplayName = "Admin",
                    UserName = "administrator",
                    State = UserState.Frozen
                });

            mock.Setup(x => x.GetByUserName("correctusername"))
               .Returns(() => new UserInfoDto()
               {
                   DisplayName = "Paul",
                   State = UserState.Active,
                   UserName = "correctusername"
               });

            #endregion

            #region Login
            mock.Setup(x => x.VerifyUserNameAndPassword("admin", "qwerty22"))
               .Returns(() => LoginStatus.IncorrectPassword);

            mock.Setup(x => x.VerifyUserNameAndPassword("adminuser1", "qwerty22"))
                .Returns(() => LoginStatus.UserFrozen);

            mock.Setup(x => x.VerifyUserNameAndPassword("adminuser2", "qwerty22"))
                .Returns(() => LoginStatus.UserNameNotExists);

            mock.Setup(x => x.VerifyUserNameAndPassword("correctusername", "1234567"))
                .Returns(() => LoginStatus.Success);
            #endregion

            #region Regist

            mock.Setup(x => x.Regist("administrator", "123456"))
                .Returns(() => RegistResult.UserNameExists);

            mock.Setup(x => x.Regist("correctusername", "123456"))
                .Returns(() => RegistResult.Success);
            #endregion


            //mock.Setup(x => x.VerifyUserNameAndPassword(It.IsAny<string>(), It.IsAny<string>()))
            //    .Returns(() => LoginStatus.IncorrectPassword);

            return mock.Object;
        }
    }
}