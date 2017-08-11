using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using com.gyt.ms.Models;
using WebGrease.Css.Extensions;
using Zer.Framework.Exception;
using Zer.Framework.Extensions;
using Zer.Framework.Mvc;
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

        /// <summary>
        /// 验证输入字符串是否有非法字符
        /// </summary>
        /// <param name="inputStrings"></param>
        public void ValidataInputString(params string[] inputStrings)
        {
            if (inputStrings.Any(x => x.CheckBadStr()))
            {
                throw new CustomException("参数含有非法字符！", "inputString", string.Join(",", inputStrings));
            }
        }

        /// <summary>
        /// 验证输入字符串是否有非法字符
        /// </summary>
        /// <param name="inputStrings"></param>
        public void ValidataInputString(params string[][] inputStrings)
        {
            inputStrings.ForEach(ValidataInputString);
        }

        /// <summary>
        /// 验证输入字符串是否有非法字符
        /// </summary>
        /// <param name="inputStrings"></param>
        public void ValidataInputString(params IEnumerable<string>[] inputStrings)
        {
            inputStrings.Select(x => x.ToArray()).ForEach(ValidataInputString);
        }

        public void ValidataInputString<T>(T obj)
        {
            var list = new List<string>();

            foreach (var property in typeof(T).GetProperties().Where(x => x.PropertyType == typeof(string)))
            {
                var str = property.GetValue(obj);
                if (str != null && !str.ToString().IsNullOrEmpty())
                {
                    list.Add(str.ToString());
                }
            }

            ValidataInputString(list.ToArray());
        }

        public T ReplaceUnsafeChar<T>(T obj)
            where T : class ,new()
        {
            var t = new T();
            foreach (var property in typeof(T).GetProperties())
            {
                var currentValue = property.GetValue(obj);
                if (currentValue == null) continue;

                var replacedValue = currentValue;

                var s = currentValue as string;
                if (s != null)
                {
                    replacedValue = s.Replace('-', '_');
                }

                property.SetValue(t, replacedValue);
            }
            return t;
        }

        protected string AppendObjectToSession(object obj)
        {
            var sessionCode = Guid.NewGuid().ToString();
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

        protected FileResult ExportCsv(byte[] data, string fileName)
        {
            // TODO:Unit Test
            return File(data, "text/plain", string.Format("{0}-{1:yyyy-MM-dd}.csv", fileName, DateTime.Now));
        }
        
        [UnLog]
        public ActionResult LeftMenu(int id)
        {
            #region 港运通业务办理
            List<MenuItem> menuItems = new List<MenuItem>();
            MenuItem businessHandle = new MenuItem();
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

            businessHandle.ChildItems = new List<MenuItem>();
            businessHandle.ChildItems.Add(businessHandleChild);
            menuItems.Add(businessHandle);
            #endregion

            #region 港运通数据管理

            MenuItem gytDataManage = new MenuItem();
            gytDataManage.Id = 2;
            gytDataManage.TextInfo = "港运通数据管理";
            gytDataManage.Icon = "icon-credit-card";

            MenuItem gytInfo = new MenuItem();
            gytInfo.Id = 3;
            gytInfo.ActionName = "Index";
            gytInfo.ControllerName = "GYTInfo";
            gytInfo.TextInfo = "港运通信息数据库";
            gytInfo.IsCurrentPage = false;
            gytInfo.Icon = "icon-file-alt";

            MenuItem gtyHandleRecrod = new MenuItem();
            gtyHandleRecrod.Id = 4;
            gtyHandleRecrod.ActionName = "Index";
            gtyHandleRecrod.ControllerName = "GYTHandleRecrod";
            gtyHandleRecrod.TextInfo = "港运通办理信息数据库";
            gtyHandleRecrod.IsCurrentPage = false;
            gtyHandleRecrod.Icon = "icon-file-alt";

            gytDataManage.ChildItems = new List<MenuItem>();
            gytDataManage.ChildItems.Add(gytInfo);
            gytDataManage.ChildItems.Add(gtyHandleRecrod);
            menuItems.Add(gytDataManage);

            #endregion

            #region 超载超限数据管理

            MenuItem overLoadDataManage = new MenuItem();
            overLoadDataManage.Id = 5;
            overLoadDataManage.TextInfo = "超载超限数据管理";
            overLoadDataManage
                .Icon = "icon-credit-card";

            MenuItem overLoadRecrod = new MenuItem();
            overLoadRecrod.Id = 6;
            overLoadRecrod.ActionName = "Index";
            overLoadRecrod.ControllerName = "PeccancyRecrod";
            overLoadRecrod.TextInfo = "超载超限信息数据库";
            overLoadRecrod.IsCurrentPage = false;
            overLoadRecrod.Icon = "icon-file-alt";

            MenuItem overLoadChange = new MenuItem();
            overLoadChange.Id = 7;
            overLoadChange.ActionName = "Index";
            overLoadChange.ControllerName = "PeccancyChangeInfo";
            overLoadChange.TextInfo = "超载超限整改信息数据库";
            overLoadChange.IsCurrentPage = false;
            overLoadChange.Icon = "icon-file-alt";

            overLoadDataManage.ChildItems = new List<MenuItem>();
            overLoadDataManage.ChildItems.Add(overLoadRecrod);
            overLoadDataManage.ChildItems.Add(overLoadChange);
            menuItems.Add(overLoadDataManage);

            #endregion

            #region LNG补贴信息数据库
            MenuItem lgnInfo = new MenuItem();
            lgnInfo.Id = 8;
            lgnInfo.TextInfo = "LNG补贴信息数据库";
            lgnInfo.Icon = "icon-credit-card";

            MenuItem lgnInfoChild = new MenuItem();
            lgnInfoChild.Id = 9;
            lgnInfoChild.ActionName = "Index";
            lgnInfoChild.ControllerName = "LngAllowance";
            lgnInfoChild.TextInfo = "LNG补贴信息数据库";
            lgnInfoChild.IsCurrentPage = false;
            lgnInfoChild.Icon = "icon-file-alt";

            lgnInfo.ChildItems = new List<MenuItem>();
            lgnInfo.ChildItems.Add(lgnInfoChild);
            menuItems.Add(lgnInfo);
            #endregion

            #region  系统管理

            MenuItem systemManage = new MenuItem();
            systemManage.Id = 10;
            systemManage.TextInfo = "系统管理";
            systemManage.Icon = "icon-cogs";

            MenuItem logInfo = new MenuItem();
            logInfo.Id = 11;
            logInfo.ActionName = "Index";
            logInfo.ControllerName = "LogInfo";
            logInfo.TextInfo = "日志管理";
            logInfo.IsCurrentPage = false;
            logInfo.Icon = "icon-file-alt";

            MenuItem accountManage = new MenuItem();
            accountManage.Id = 12;
            accountManage.ActionName = "AccountManage";
            accountManage.ControllerName = "User";
            accountManage.TextInfo = "账户管理";
            accountManage.IsCurrentPage = false;
            accountManage.Icon = "icon-file-alt";

            MenuItem changPassword = new MenuItem();
            changPassword.Id = 13;
            changPassword.ActionName = "ChangePassword";
            changPassword.ControllerName = "User";
            changPassword.TextInfo = "修改密码";
            changPassword.IsCurrentPage = false;
            changPassword.Icon = "icon-file-alt";

            systemManage.ChildItems = new List<MenuItem>();
            systemManage.ChildItems.Add(logInfo);
            systemManage.ChildItems.Add(accountManage);
            systemManage.ChildItems.Add(changPassword);
            menuItems.Add(systemManage);

            #endregion

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