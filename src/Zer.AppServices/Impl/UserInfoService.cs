using System;
using System.Collections.Generic;
using AutoMapper;
using Zer.Entities;
using Zer.GytDataService;
using Zer.GytDto.Users;

namespace Zer.AppServices.Impl
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IUserInfoDataService _userIbfoDataService;

        public UserInfoService(IUserInfoDataService userIbfoDataService)
        {
            _userIbfoDataService = userIbfoDataService;
        }

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

        public UserInfoDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserInfoDto> GetAll()
        {
            return Mapper.Map<List<UserInfoDto>>(_userIbfoDataService.GetAll());
        }

        public UserInfoDto Add(UserInfoDto model)
        {
            throw new NotImplementedException();
        }

        public List<UserInfoDto> AddRange(List<UserInfoDto> list)
        {
            throw new NotImplementedException();
        }
    }
}
