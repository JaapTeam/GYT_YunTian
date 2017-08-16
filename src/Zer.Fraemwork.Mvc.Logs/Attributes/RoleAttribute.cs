using System;
using Zer.Entities;

namespace Zer.Framework.Mvc.Logs.Attributes
{
    public class AdminRoleAttribute : Attribute
    {
        public AdminRoleAttribute()
        {
            RoleLevel = RoleLevel.Administrator;
        }

        public AdminRoleAttribute(RoleLevel roleLevel)
        {
            RoleLevel = roleLevel;
        }

        public RoleLevel RoleLevel { get; private set; }
    }
}