﻿namespace Zer.Services.Users.Dto
{
    public enum LoginStatus
    {
        Success=0,
        IncorrectPassword=1,
        UserFrozen=2,
        UserNameNotExists = 3,
    }
}