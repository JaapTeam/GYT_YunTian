using Zer.Entities;
using Zer.Framework.Attributes;
using Zer.Framework.Dto;

namespace Zer.GytDto.Users
{
    public class UserInfoDto : DtoBase
    {
        public UserInfoDto()
        {
            Role = RoleLevel.User;
        }

        public int UserId { get ; set; }
        public string UserName { get; set; }

        // TODO: 密码不能输出到运行时内存呢

        public string Password { get; set; }

        public string DisplayName { get; set; }
        public UserState UserState { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public RoleLevel Role { get; set; }
    }
}