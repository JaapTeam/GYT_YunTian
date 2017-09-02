using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Zer.Entities;
using Zer.Framework.Entities;
using Zer.Framework.Exception;
using Zer.GytDataService;
using Zer.GytDto.Extensions;
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
            return _userIbfoDataService.Single(x => x.UserName == userName).Map<UserInfoDto>();
        }

        public LoginStatus VerifyUserNameAndPassword(string userName, string password)
        {
            var userInfo = _userIbfoDataService.Single(x => x.UserName == userName);
            if(userInfo == null) return LoginStatus.UserNameNotExists;
            if(userInfo.UserState == UserState.Frozen) return LoginStatus.UserFrozen;
            return userInfo.Password == password ? LoginStatus.Success : LoginStatus.IncorrectPassword;
        }

        public RegistResult Regist(UserInfoDto userInfo)
        {
            if (Exists(userInfo.UserName))
            {
                return RegistResult.UserNameExists;
            }

            var entity = Mapper.Map<UserInfo>(userInfo);

            if (_userIbfoDataService.Insert(entity) == null)
            {
                throw new CustomException("注册失败",new Dictionary<string,string>{{"UserName",userInfo.UserName}});
            }

            return RegistResult.Success;
        }

        public FrozenResult LetUserFrozen(int userId)
        {
            var entity = _userIbfoDataService.GetById(userId);

            entity.UserState = UserState.Frozen;

            return _userIbfoDataService.Update(entity)!=null ? FrozenResult.Success : FrozenResult.UserIsFrzon;
        }

        public ThawResult LetUserThaw(int userId)
        {
            var entity = _userIbfoDataService.GetById(userId);

            entity.UserState = UserState.Active;

            return _userIbfoDataService.Update(entity) != null ? ThawResult.Success : ThawResult.UserIsThaw;
        }

        public ChangePasswordResult ChangePassword(int userId, string newPassword)
        {
            var entity = _userIbfoDataService.GetById(userId);

            entity.Password = newPassword;

            return _userIbfoDataService.Update(entity) != null ? ChangePasswordResult.Success : ChangePasswordResult.SameAsOldPassword;
        }

        public bool Exists(string  userName)
        {
            return _userIbfoDataService.GetAll().Any(x => x.UserName == userName);
        }

        public void SetUserRole(int userId, RoleLevel role)
        {
            _userIbfoDataService.Update(userId,x=>x.Role = role);
        }


        public UserInfoDto GetById(int id)
        {
            return _userIbfoDataService.GetById(id).Map<UserInfoDto>();
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

        public UserInfoDto Edit(UserInfoDto model)
        {
            var entity = _userIbfoDataService.GetById(model.UserId);

            entity.UserName = model.UserName;
            entity.DisplayName = model.DisplayName;
            entity.Email = model.Email;
            entity.MobilePhone = model.MobilePhone;

            return _userIbfoDataService.Update(entity).Map<UserInfoDto>();
        }
    }
}
