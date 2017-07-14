using Zer.Entities.User;

namespace Zer.Services.Users.Dto
{
    public class UserInfoDto
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public UserState State { get; set; }
    }
}
