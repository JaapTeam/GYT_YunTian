
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
    [RoutePrefix("u")]
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
        [Route("r")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Regist(UserInfoDto userInfoDto)
        {
            if (userInfoDto.UserName.IsNullOrEmpty() ||
                userInfoDto.Password.IsNullOrEmpty())
            {
                return Fail("用户名或密码不能为空！");
            }
            
            if (userInfoDto.UserName.Length < 6 || userInfoDto.Password.Length < 6)
            {
                return Fail("用户名或密码的长度不能小于6！");
            }

            userInfoDto.Password = userInfoDto.Password.Md5Encoding();
            userInfoDto.UserState = UserState.Frozen;

            var registResult = _userInfoService.Regist(userInfoDto);

            if (registResult == RegistResult.UserNameExists)
            {
                return Fail("用户名已经存在!");
            }

            return Success();
        }

        [UnValidateLogin]
        [UnLog]
        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public JsonResult Login(string userName, string psd)
        {
            //var loginResult = _userInfoService.VerifyUserNameAndPassword(userName, psd.Md5Encoding());
            var userinfoDto = _userInfoService.GetByUserName(userName,psd.Md5Encoding());

            var logInfoDto = new LogInfoDto()
            {
                ActionModel = "登录",
                ActionType = ActionType.查询,
                CreateTime = DateTime.Now
            };
            
            if (userinfoDto == null)
            {
                logInfoDto.Content = string.Format("UserName:{0},result:用户名或密码错误", userName);
                UserActionLogger.Instance.Info(logInfoDto);
                return Fail("用户名或密码错误");
            }

            if (userinfoDto.UserState == UserState.Frozen)
            {
                logInfoDto.Content = string.Format("UserName:{0},result:{1}", userName, LoginStatus.UserFrozen);
                UserActionLogger.Instance.Info(logInfoDto);
                return Fail("用户被冻结.");
            }
            
            logInfoDto.Content = string.Format("UserName:{0},result:{1}", userName, LoginStatus.Success);
            UserActionLogger.Instance.Info(logInfoDto);

            Session["UserInfo"] = userinfoDto;
            return Success();
        }

        [Route("out")]
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
        [Route("f")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Frozon(int userId)
        {
            var userInfoDto = _userInfoService.GetById(userId);

            if (userInfoDto.UserState != UserState.Active)
            {
                return Success();
            }

            var frozonResult = _userInfoService.LetUserFrozen(userId);

            if (frozonResult == FrozenResult.Success)
            {
                return Success();
            }

            return Fail("用户状态修改失败");
        }

        [AdminRole]
        [UserActionLog("用户状态变更-->激活", ActionType.更改状态)]
        [Route("raw")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Thaw(int userId)
        {
            var userInfoDto = _userInfoService.GetById(userId);

            if (userInfoDto.UserState != UserState.Frozen)
            {
                return Success();
            }

            var frozonResult = _userInfoService.LetUserThaw(userId);

            if (frozonResult == ThawResult.Success)
            {
                return Success();
            }

            return Fail("用户状态修改失败");
        }

        [UserActionLog("用户修改密码", ActionType.编辑)]
        [Route("changepwd")]
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        [Route("cpw")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Route("accuntmgr")]
        public ActionResult AccountManage()
        {
            ViewBag.Result = _userInfoService.GetAll();
            return View();
        }

        [AdminRole]
        [UserActionLog("查看用户详细信息", ActionType.编辑)]
        [Route("accinfo/{userId}")]
        public ActionResult AccountInfo(int userId = 0)
        {
            ViewBag.UserInfo = _userInfoService.GetById(userId);
            return View();
        }

        [Route("au")]
        public ActionResult AddUserInfo()
        {
            return View();
        }

        [Route("editu/{userId}")]
        public ActionResult EditUserInfo(int userId = 0)
        {
            ViewBag.Result = _userInfoService.GetById(userId);
            return View();
        }

        [AdminRole]
        [UserActionLog("编辑个人信息", ActionType.编辑)]
        [Route("uedit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
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

            //dto.UserName = userInfoDto.UserName;
            dto.DisplayName = userInfoDto.DisplayName;
            dto.Email = userInfoDto.Email;
            dto.MobilePhone = userInfoDto.MobilePhone;

            _userInfoService.Edit(dto);

            return Success();
        }

        [AdminRole]
        [UserActionLog("设置用户权限", ActionType.编辑)]
        [Route("role/set")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SetRole(int userId, RoleLevel roleId)
        {
            _userInfoService.SetUserRole(userId,roleId);
            return Success();
        }
    }
}