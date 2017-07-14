
using System;
using System.Web.Helpers;
using System.Web.Mvc;
using Zer.Services.Users;
using Zer.Services.Users.Dto;
using Zer.Framework.Extensions;


namespace com.gyt.ms.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserInfoService _userInfoService;
        public UserController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        public ActionResult Regist(string userName, string password)
        {
            if (userName.CheckBadStr() || password.CheckBadStr())
            {
                throw new ArgumentException("参数含有非法字符！");
            }

            if (userName.IsNullOrEmpty() ||
                password.IsNullOrEmpty() ||
                userName.Length <= 6 ||
                password.Length < 6)
            {
                return View("Error");
            }

            var registResult = _userInfoService.Regist(userName, password);

            if (registResult == RegistResult.UserNameExists)
            {
                return View("Error");
            }

            return View("Login");
        }

        public ActionResult Login(string userName, string password)
        {
            if (userName.CheckBadStr() || password.CheckBadStr())
            {
                throw new ArgumentException("参数含有非法字符！");
            }

            var loginResult = _userInfoService.VerifyUserNameAndPassword(userName, password);

            if (loginResult != LoginStatus.Success)
            {
                return View("Error");
            }
            var userinfoDto = _userInfoService.GetByUserName(userName);
            
            // Todo:写不进Session 
            //Session["UserInfo"] = userinfoDto;
            return View("Success");
        }

        public JsonResult Logout()
        {
            // TODO: unit test coding
            Session["UserInfo"] = null;
            return Success();
        }

        public JsonResult Frozon(int userId)
        {
            var frozonResult = _userInfoService.LetUserFrozen(userId);

            if (frozonResult == FrozenResult.Success)
            {
                return Success(FrozenResult.Success);
            }

            return Fail();
        }

        public JsonResult Thaw(int userId)
        {
            var frozonResult = _userInfoService.LetUserThaw(userId);

            if (frozonResult == ThawResult.Success)
            {
                return Success();
            }

            return Fail();
        }
    }
}