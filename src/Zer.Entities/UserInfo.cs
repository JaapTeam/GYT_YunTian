using System;
using System.ComponentModel.DataAnnotations;
using Zer.Framework.Entities;

namespace Zer.Entities
{
    [Serializable]
    public class UserInfo : EntityBase
    {
        [StringLength(20)]
        public string UserName { get; set; }
        [StringLength(20)]
        public string DisplayName { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        public UserState UserState { get; set; }
        [StringLength(40)]
        public string Email { get; set; }
        [StringLength(20)]
        public string MobilePhone { get; set; }
        /// <summary>
        /// 权限级别
        /// </summary>
        public RoleLevel Role { get; set; }
    }

    public enum RoleLevel
    {
        /// <summary>
        /// 普通用户
        /// </summary>
        User = 0,

        /// <summary>
        /// 管理员
        /// </summary>
        Administrator = 1
        
    }

    public enum UserState
    {
        Active = 0,
        Frozen = 1
    }
}
