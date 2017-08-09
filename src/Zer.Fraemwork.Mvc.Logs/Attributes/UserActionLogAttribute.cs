using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Zer.GytDto;

namespace Zer.Framework.Mvc.Logs.Attributes
{
    public class UserActionLogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if(filterContext.ActionDescriptor.get)
            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(UnLogAttribute), false).Any())
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            // TODO: 完善用户信息获取
            var logInfoDto = new LogInfoDto();
            logInfoDto.UserId = 1;
            logInfoDto.DisplayName = "测试用户";
            logInfoDto.ActionModel = string.Format("{0}/{1}",
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                filterContext.ActionDescriptor.ActionName
            );

            StringBuilder paramStringBuilder = new StringBuilder();

            foreach (var parametersKey in filterContext.ActionParameters.Keys)
            {
                paramStringBuilder.AppendFormat("{0}:{1}|", parametersKey,
                    filterContext.ActionParameters[parametersKey]);
            }

            logInfoDto.Content = paramStringBuilder.ToString();
            logInfoDto.CreateTime = DateTime.Now;
            logInfoDto.IP = IpHelper.GetIp();

            UserActionLogger.Instance.Info(logInfoDto);

            //var userInfodto = filterContext.HttpContext.Session["UserInfo"] as UserInfoDto;

            //if (userInfodto == null)
            //{
            //    filterContext.Result =new RedirectToRouteResult(new RouteValueDictionary(new
            //    {
            //        Controller = "User",
            //        Action = "index"
            //    }));
            //}

            base.OnActionExecuting(filterContext);
        }
    }
}