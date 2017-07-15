using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zer.Framework.Extensions;
using Zer.Framework.Mvc;
using Zer.Services.Users.Dto;

namespace com.gyt.ms.Controllers
{
    public class BaseController : Controller
    {
        public UserInfoDto CurrentUser
        {
            get
            {
                if (HttpContext == null || HttpContext.Session == null)
                    return null;

                return HttpContext.Session["UserInfo"] as UserInfoDto;
            }
        }

        /// <summary>
        /// 验证输入字符串是否有非法字符
        /// </summary>
        /// <param name="inputStrings"></param>
        protected void ValidataInputString(params string[] inputStrings)
        {
            if (inputStrings.Any(x => x.CheckBadStr()))
            {
                throw new ArgumentException("参数含有非法字符！");
            }
        }

        protected JsonResult Success()
        {
            return Success("");
        }

        protected JsonResult Success(string msg)
        {
            return Success("", msg);
        }

        protected JsonResult Success(object data, string msg = "")
        {
            return Json(new { C = ResultCode.Success.ToInt(), msg, data }, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult Fail(string msg = "")
        {
            return Json(new { C = ResultCode.Fail.ToInt(), msg = msg, data = "" }, JsonRequestBehavior.AllowGet);
        }
    }
}