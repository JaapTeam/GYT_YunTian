using System.ComponentModel.DataAnnotations;
using Zer.Framework.Entities;

namespace Zer.Entities
{
    public class UserInfo : EntityBase
    {
        [MaxLength(20)]
        public string UserName { get; set; }
        [MaxLength(20)]
        public string DisplayName { get; set; }
        [MaxLength(40)]
        public string Password { get; set; }
        public UserState UserState { get; set; }
        [MaxLength(40)]
        public string Email { get; set; }
        [MaxLength(40)]
        public string MobilePhone { get; set; }

        public RoleLevel Role { get; set; }
    }

    public enum RoleLevel
    {
        Administrator,
        DataAdmin,
        User
    }

    public enum UserState
    {
        Active = 0,
        Frozen = 1
    }
}
