using Zer.GytDto.Users;

namespace Zer.Services
{
    public interface IUserInfoService : IDomainService<UserInfoDto, int>
    {
        UserInfoDto GetByUserName(string userName);
        LoginStatus VerifyUserNameAndPassword(string userName, string password);

        RegistResult Regist(string userName, string password);

        FrozenResult LetUserFrozen(int userId);

        ThawResult LetUserThaw(int userId);

        ChangePasswordResult ChangePassword(int userId, string newPassword);
    }
}
