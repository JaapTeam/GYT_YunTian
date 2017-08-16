using System;
using System.Net;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Zer.Framework.Exception;
using Zer.Framework.Extensions;

namespace Zer.Framework.Mvc.Logs.Attributes
{
    public class CustomExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        //private static readonly ISystemLogInfoDataService Logger;
        private readonly string _viewName;

        static CustomExceptionAttribute()
        {
            //Logger = IocManager.Instance.Resolve<ISystemLogInfoDataService>();
        }

        public CustomExceptionAttribute(string viewName)
        {
            _viewName = viewName;
        }

        public void OnException(ExceptionContext filterContext)
        {
            //var exceptionMessage = filterContext.Exception.Message;
            //var controllerName = (string)filterContext.RouteData.Values["controller"];
            //var actionName = (string)filterContext.RouteData.Values["action"];

            //var systemLog = new SystemLogInfo();
            //systemLog.ActionName = actionName;
            //systemLog.ControllerName = controllerName;
            //systemLog.Content = exceptionMessage;

            //Logger.Insert(systemLog);

            //MailHelper mail = new MailHelper();

            var exception = filterContext.Exception;


            var baseException = exception as BaseException;
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                AjaxException(filterContext, exception);
                return;
            }

            //mail.SendAsync(exception.Message, exception.StackTrace, ConfigurationManager.AppSettings["ExceptionCollectMail"], null);

            var view = new ViewResult();
            view.ViewBag.Exception = exception;
            view.ViewName = _viewName;

            filterContext.Result = view;
            filterContext.ExceptionHandled = true;
        }

        private void AjaxException(ExceptionContext filterContext, System.Exception exception)
        {
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            filterContext.HttpContext.Response.ContentType = "application/json; charset=utf-8";

            var camelCaseFormatter = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var data = new JsonData { C = ResultCode.Fail.ToInt(), Msg = exception.Message };

            filterContext.HttpContext.Response.Write(JsonConvert.SerializeObject(data, camelCaseFormatter));
            filterContext.HttpContext.Response.Flush();
            filterContext.HttpContext.Response.End();
        }
    }
}