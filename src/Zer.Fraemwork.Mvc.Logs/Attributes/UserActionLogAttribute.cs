using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Zer.Entities;
using Zer.Framework.Helpers;
using Zer.GytDto;
using Zer.GytDto.Users;

namespace Zer.Framework.Mvc.Logs.Attributes
{
    public class UserActionLogAttribute : ActionFilterAttribute
    {
        public UserActionLogAttribute(string actionModel, ActionType actionType)
        {
            ActionModel = actionModel;
            ActionType = actionType;
        }

        private string ActionModel { get; set; }
        private ActionType ActionType { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(UnLogAttribute), false).Any())
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            var logInfoDto = new LogInfoDto();

            logInfoDto.UserId = -1;
            logInfoDto.DisplayName = "非法用户";

            if (filterContext.HttpContext.Session != null)
            {
                var userInfoDto = filterContext.HttpContext.Session["UserInfo"] as UserInfoDto;
                if (userInfoDto == null)
                {
                    filterContext.Result = new RedirectResult("~/home/login");
                    return;
                }

                logInfoDto.UserId = userInfoDto.UserId;
                logInfoDto.DisplayName = userInfoDto.DisplayName;
            }

            logInfoDto.ActionModel = ActionModel;
            logInfoDto.ActionType = ActionType;

            StringBuilder paramStringBuilder = new StringBuilder();

            foreach (var parametersKey in filterContext.ActionParameters.Keys)
            {
                paramStringBuilder.AppendFormat("{0}:{1}|", parametersKey,
                    filterContext.ActionParameters[parametersKey]);
            }

            logInfoDto.Content = paramStringBuilder.ToString();
            logInfoDto.CreateTime = DateTime.Now;
            logInfoDto.IP = IpHelper.GetWebClientIp();

            UserActionLogger.Instance.Info(logInfoDto);
            
            base.OnActionExecuting(filterContext);
        }
    }
}

