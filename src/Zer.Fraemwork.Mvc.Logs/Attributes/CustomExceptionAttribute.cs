using System;
using System.Web.Mvc;
using System.Web.Routing;
using Zer.Entities;
using Zer.Framework.Dependency;
using Zer.GytDataService;

namespace Zer.Framework.Mvc.Logs.Attributes
{
    public class CustomExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        private static readonly ISystemLogInfoDataService Logger;

        static CustomExceptionAttribute()
        {
            Logger = IocManager.Instance.Resolve<ISystemLogInfoDataService>();
        }

        public void OnException(ExceptionContext filterContext)
        {
            var exceptionMessage = filterContext.Exception.Message;
            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            
            var systemLog = new SystemLogInfo();
            systemLog.ActionName = actionName;
            systemLog.ControllerName = controllerName;
            systemLog.Content = exceptionMessage;

            Logger.Insert(systemLog);

            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "home", Action = "Error", msg = exceptionMessage }));
            filterContext.ExceptionHandled = true;
        }
    }
}