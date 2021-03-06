﻿using Zer.Entities;
using Zer.GytDto.Users;

namespace Zer.AppServices
{
    public interface IUserInfoService : AppServices.IAppService<UserInfoDto,int>
    {
        UserInfoDto GetByUserName(string userName,string psd);
        LoginStatus VerifyUserNameAndPassword(string userName, string password);

        RegistResult Regist(UserInfoDto userInfo);

        FrozenResult LetUserFrozen(int userId);

        ThawResult LetUserThaw(int userId);

        ChangePasswordResult ChangePassword(int userId, string newPassword);

        bool Exists(string userName);

        void SetUserRole(int userId, RoleLevel role);
    }
}
