using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.gyt.ms.Models;
using WebGrease.Css.Extensions;
using Zer.Framework.Extensions;
using Zer.Framework.Mvc;
using Zer.GytDto.UserInfo;

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
        public void ValidataInputString(params string[] inputStrings)
        {
            if (inputStrings.Any(x => x.CheckBadStr()))
            {
                throw new ArgumentException("参数含有非法字符！");
            }
        }

        /// <summary>
        /// 验证输入字符串是否有非法字符
        /// </summary>
        /// <param name="inputStrings"></param>
        public void ValidataInputString(params string[][] inputStrings)
        {
            // TODO:Unit Test
            inputStrings.ForEach(ValidataInputString);
        }

        /// <summary>
        /// 验证输入字符串是否有非法字符
        /// </summary>
        /// <param name="inputStrings"></param>
        public void ValidataInputString(params IEnumerable<string>[] inputStrings)
        {
            // TODO:Unit Test
            inputStrings.Select(x => x.ToArray()).ForEach(ValidataInputString);
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

        protected FileResult ExportCsv(byte[] data, string fileName)
        {
            // TODO:Unit Test
            return File(data, "text/plain", string.Format("{0}-{1:yyyy-MM-dd}.csv",fileName, DateTime.Now));
        }

        public ActionResult LeftMenu(int id = 0)
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            MenuItem businessHandle=new MenuItem();
            businessHandle.Id = 0;
            businessHandle.TextInfo = "港运通业务办理";
            businessHandle.Icon = "icon-briefcase";

            MenuItem businessHandleChild = new MenuItem();
            businessHandleChild.Id = 1;
            businessHandleChild.ActionName = "Index";
            businessHandleChild.ControllerName = "BusinessHandle";
            businessHandleChild.TextInfo = "港运通业务办理";
            businessHandleChild.IsCurrentPage = false;
            businessHandleChild.Icon = "icon-briefcase";

            businessHandle.ChildItems=new List<MenuItem>();
            businessHandle.ChildItems.Add(businessHandleChild);
            menuItems.Add(businessHandle);

            foreach (var item in menuItems)
            {
                if (item.ChildItems == null) continue;
                foreach (var child in item.ChildItems)
                {
                    if (child.Id == id)
                    {
                        item.IsCurrentPage = true;
                        child.IsCurrentPage = true;
                    }
                }
            }

            ViewBag.MenuItems = menuItems;
            return PartialView("LeftMenu");
        }
    }
}