using Zer.Entities;
using Zer.Framework.Attributes;

namespace Zer.GytDto.Users
{
    public class UserInfoDto : IValidateInputParameter
    {
        public int UserId { get ; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public UserState State { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }

    }
}