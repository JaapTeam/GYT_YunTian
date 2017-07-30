using System;
using System.Collections.Generic;
using Zer.GytDto;
using Zer.GytDto.UserInfo;

namespace Zer.Services.Impl
{
    public class UserInfoService :IUserInfoService
    {
        public UserInfoDto GetByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public LoginStatus VerifyUserNameAndPassword(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public RegistResult Regist(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public FrozenResult LetUserFrozen(int userId)
        {
            throw new NotImplementedException();
        }

        public ThawResult LetUserThaw(int userId)
        {
            throw new NotImplementedException();
        }

        public ChangePasswordResult ChangePassword(int userId, string newPassword)
        {
            throw new NotImplementedException();
        }

        public  UserInfoDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public  List<UserInfoDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public  bool Add(UserInfoDto model)
        {
            throw new NotImplementedException();
        }

        public  bool AddRange(List<UserInfoDto> list)
        {
            throw new NotImplementedException();
        }
    }
}
