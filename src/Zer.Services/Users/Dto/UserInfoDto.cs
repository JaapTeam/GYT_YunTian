using Zer.Entities.User;

namespace Zer.Services.Users.Dto
{
    public class UserInfoDto
    {
        // TODO: 输出实体中没有Id属性，但是在服务中有用到userinfo 的Id 属性，请在此添加
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public UserState State { get; set; }
    }
}
