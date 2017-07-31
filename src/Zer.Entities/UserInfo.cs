using Zer.Framework.Entities;

namespace Zer.Entities
{
    public class UserInfo : UserInfoBase
    {
        public UserState UserState { get; set; }
    }

    public enum UserState
    {
        Active = 0,
        Frozen = 1
    }
}
