using System;
using Zer.Entities;

namespace Zer.Framework.Mvc.Logs.Attributes
{
    public class RoleAttribute : Attribute
    {
        public RoleAttribute()
        {
            RoleLevel = RoleLevel.Administrator;
        }

        public RoleAttribute(RoleLevel roleLevel)
        {
            RoleLevel = roleLevel;
        }

        public RoleLevel RoleLevel { get; private set; }
    }
}