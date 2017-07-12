﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Entities.User;
using Zer.Services.Users.Dto;

namespace Zer.Services.Users
{
    public interface IUserInfoService
    {

        UserInfoDto GetById(int userId);
        UserInfoDto GetByUserName(string userName);
        LoginStatus VerifyUserNameAndPassword(string userName, string password);

        RegistResult Regist(string userName, string password);

        bool ChangePassword(int userId, string newPassword);
    }
}
