﻿using Zer.Entities;

namespace Zer.GytDto.Users
{
    public class UserInfoDto
    {
        public int UserId { get ; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public UserState State { get; set; }
    }
}