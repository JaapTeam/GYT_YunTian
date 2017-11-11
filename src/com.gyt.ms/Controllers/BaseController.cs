using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.gyt.ms.Models;
using Zer.Framework.Extensions;
using Zer.Framework.Mvc;
using Zer.Framework.Mvc.Logs;
using Zer.Framework.Mvc.Logs.Attributes;
using Zer.GytDto.Users;

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

        protected virtual void SaveFile(HttpPostedFileBase file,string dirPath)
        {
            try
            {
                var filename = string.Format("{0}/{1:yyyy-MM-dd_HH_mm_ss_fffff}_{3}.{2}",
                    dirPath, DateTime.Now, Guid.NewGuid(), CurrentUser.UserId);
                file.SaveAs(Server.MapPath("~/content/uploadFiles/" + filename));
            }
            catch
            {
            }
        }

        protected string AppendObjectToSession(object obj)
        {
            var sessionCode = Guid.NewGuid().ToString().Replace('-', '_');
            Session[sessionCode] = obj;
            return sessionCode;
        }

        /// <summary>
        /// 从Session中获取指定值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionCode"></param>
        /// <returns></returns>
        protected T GetValueFromSession<T>(string sessionCode)
        {
            return (T)Session[sessionCode];
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
            return Json(new { C = ResultCode.Fail.ToInt(), msg, data = "" }, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult Fail(string msg, object data)
        {
            return Json(new { C = ResultCode.Fail.ToInt(), msg, data }, JsonRequestBehavior.AllowGet);
        }

        protected FileResult ExportCsv(byte[] data, string fileName)
        {
            // TODO:Unit Test
            return File(data, "text/plain", string.Format("{0}-{1:yyyy-MM-dd}.csv", fileName, DateTime.Now));
        }

    }
}