namespace Zer.Entities.User
{
    public class UserInfo : EntityBase
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public UserState UserState { get; set; }
    }

    public enum UserState
    {
        Active = 0,
        Frozen = 1
    }
}
