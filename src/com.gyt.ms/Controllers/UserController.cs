
using System;
using System.Web.Mvc;
using Zer.AppServices;
using Zer.Entities;
using Zer.Framework.Exception;
using Zer.Framework.Extensions;
using Zer.Framework.Mvc.Logs;
using Zer.Framework.Mvc.Logs.Attributes;
using Zer.GytDto;
using Zer.GytDto.Users;
using ActionType = Zer.Entities.ActionType;


namespace com.gyt.ms.Controllers
{
    public class UserController : BaseController
    {
        public UserController()
        {

        }

        private readonly IUserInfoService _userInfoService;
        public UserController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        [AdminRole]
        [UserActionLog("新增用户", ActionType.新增)]
        public JsonResult Regist(UserInfoDto userInfoDto)
        {
            if (userInfoDto.UserName.IsNullOrEmpty() ||
                userInfoDto.Password.IsNullOrEmpty())
            {
                throw new ArgumentException("用户名或密码不能为空！");
            }

            if (
                userInfoDto.UserName.Length <= 6 ||
                userInfoDto.Password.Length < 6)
            {
                throw new ArgumentException("用户名或密码的长度不能小于6！");
            }

            userInfoDto.Password = userInfoDto.Password.Md5Encoding();
            userInfoDto.UserState = UserState.Frozen;

            var registResult = _userInfoService.Regist(userInfoDto);

            if (registResult == RegistResult.UserNameExists)
            {
                return Fail();
            }

            return Success();
        }

        [UnValidateLogin]
        [UnLog]
        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            var loginResult = _userInfoService.VerifyUserNameAndPassword(userName, password.Md5Encoding());

            UserActionLogger.Instance.Info(new LogInfoDto()
            {
                ActionModel = "登录",
                ActionType =  ActionType.查询,
                Content = string.Format("UserName:{0}, Result:{1}",userName,loginResult),
                CreateTime = DateTime.Now
            });

            switch (loginResult)
            {
                case LoginStatus.IncorrectPassword: return Fail("密码错误.");
                case LoginStatus.UserFrozen: return Fail("用户被冻结.");
                case LoginStatus.UserNameNotExists: return Fail("用户名不存在.");
                default:
                {
                    var userinfoDto = _userInfoService.GetByUserName(userName);

                    Session["UserInfo"] = userinfoDto;
                    return Success();
                }
            }
        }

        public ActionResult Logout()
        {
            var userInfoDto = GetValueFromSession<UserInfoDto>("UserInfo");

            UserActionLogger.Instance.Info(new LogInfoDto()
            {
                ActionModel = "登出/注销",
                ActionType = ActionType.更改状态,
                Content = string.Format("用户名:{0}, 真实姓名:{1}", userInfoDto.UserName, userInfoDto.DisplayName),
                CreateTime = DateTime.Now
            });

            Session["UserInfo"] = null;
            return RedirectToAction("Login", "Home");
        }

        [AdminRole]
        [UserActionLog("用户状态变更-->冻结", ActionType.更改状态)]
        public ActionResult Frozon(int userId)
        {
            var userInfoDto = _userInfoService.GetById(userId);

            if (userInfoDto.UserState != UserState.Active)
            {
                return RedirectToAction("AccountManage", "User");
            }

            var frozonResult = _userInfoService.LetUserFrozen(userId);

            if (frozonResult == FrozenResult.Success)
            {
                return RedirectToAction("AccountManage", "User");
            }

            throw new CustomException("用户状态修改失败");
        }

        [AdminRole]
        [UserActionLog("用户状态变更-->激活", ActionType.更改状态)]
        public ActionResult Thaw(int userId)
        {
            var userInfoDto = _userInfoService.GetById(userId);

            if (userInfoDto.UserState != UserState.Frozen)
            {
                return RedirectToAction("AccountManage", "User");
            }

            var frozonResult = _userInfoService.LetUserThaw(userId);

            if (frozonResult == ThawResult.Success)
            {
                return RedirectToAction("AccountManage", "User");
            }

            throw new CustomException("用户状态修改失败");
        }

        [UserActionLog("用户修改密码", ActionType.编辑)]
        public JsonResult ChangePasswrod(string newPassword, string currentPassword)
        {
            newPassword = newPassword.Md5Encoding();
            currentPassword = currentPassword.Md5Encoding();

            if (currentPassword != CurrentUser.Password)
            {
                return Fail("当前密码错误！");
            }

            var userInfoDto = _userInfoService.GetById(CurrentUser.UserId);

            if (userInfoDto.Password == newPassword)
            {
                return Fail("修改失败，与旧密码相同！");
            }

            var changePasswordResult = _userInfoService.ChangePassword(CurrentUser.UserId, newPassword);

            if (changePasswordResult == ChangePasswordResult.Success)
            {
                CurrentUser.Password = newPassword;
                return Success();
            }

            return Fail();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult AccountManage()
        {
            ViewBag.Result = _userInfoService.GetAll();
            return View();
        }

        [AdminRole]
        [UserActionLog("查看用户详细信息", ActionType.编辑)]
        public ActionResult AccountInfo(int userId = 0)
        {
            ViewBag.UserInfo = _userInfoService.GetById(userId);
            return View();
        }

        public ActionResult AddUserInfo()
        {
            return View();
        }

        public ActionResult EditUserInfo(int userId = 0)
        {
            ViewBag.Result = _userInfoService.GetById(userId);
            return View();
        }

        [AdminRole]
        [UserActionLog("编辑个人信息", ActionType.编辑)]
        public JsonResult Edit(UserInfoDto userInfoDto)
        {
            if (userInfoDto.UserName.IsNullOrEmpty())
            {
                throw new ArgumentException("用户名不能为空！");
            }

            if (userInfoDto.UserName.Length <= 6)
            {
                throw new ArgumentException("用户名的长度不能小于6！");
            }

            var dto = _userInfoService.GetById(userInfoDto.UserId);

            if (dto == null)
            {
                throw new ArgumentException("未找到对应的用户！");
            }

            dto.UserName = userInfoDto.UserName;
            dto.DisplayName = userInfoDto.DisplayName;
            dto.Email = userInfoDto.Email;
            dto.MobilePhone = userInfoDto.MobilePhone;

            _userInfoService.Edit(dto);

            return Success();
        }

        [AdminRole]
        [UserActionLog("设置用户权限", ActionType.编辑)]
        public ActionResult SetRole(int userId, RoleLevel roleId)
        {
            _userInfoService.SetUserRole(userId,roleId);
            return RedirectToAction("AccountManage", "User");
        }
    }
}