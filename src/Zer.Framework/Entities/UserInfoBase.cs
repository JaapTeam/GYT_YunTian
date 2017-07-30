using System;

namespace Zer.Framework.Entities
{
    public abstract class UserInfoBase : EntityBase, ICreateTime
    {
        public DateTime CreateDateTime { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
    }
}