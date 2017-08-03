using Zer.GytDto.Users;

namespace Zer.AppServices
{
    public interface IUserInfoService : AppServices.IDomainService<UserInfoDto, int>
    {
        UserInfoDto GetByUserName(string userName);
        LoginStatus VerifyUserNameAndPassword(string userName, string password);

        RegistResult Regist(string userName, string password);

        FrozenResult LetUserFrozen(int userId);

        ThawResult LetUserThaw(int userId);

        ChangePasswordResult ChangePassword(int userId, string newPassword);
    }
}
