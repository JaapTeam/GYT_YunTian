using System.Collections.Generic;
using Zer.Framework;
using Zer.GytDto;
using Zer.GytDto.UserInfo;

namespace Zer.Services
{
    public interface IUserInfoService : IAppService
    {
        UserInfoDto GetByUserName(string userName);
        LoginStatus VerifyUserNameAndPassword(string userName, string password);

        RegistResult Regist(string userName, string password);

        FrozenResult LetUserFrozen(int userId);

        ThawResult LetUserThaw(int userId);

        ChangePasswordResult ChangePassword(int userId, string newPassword);

        UserInfoDto GetById(int id);

        List<UserInfoDto> GetAll();

        bool Add(UserInfoDto model);

        bool AddRange(List<UserInfoDto> list);
    }
}
