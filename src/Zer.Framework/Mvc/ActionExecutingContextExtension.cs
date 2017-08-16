using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Zer.Framework.Exception;

namespace Zer.Framework.Mvc
{
    public static class ActionExecutingContextExtension
    {
        public static List<T> GetActionAttribute<T>(this ActionExecutingContext filterContext, bool inhert = false)
            where T : Attribute
        {
            return filterContext.ActionDescriptor.GetCustomAttributes(typeof(T), inhert).Cast<T>().ToList();
        }

        public static List<T> GetControllerAttribute<T>(this ActionExecutingContext filterContext, bool inhert = false)
            where T : Attribute
        {
            return filterContext.ActionDescriptor
                .ControllerDescriptor
                .GetCustomAttributes(typeof(T), inhert).Cast<T>()
                .ToList();
        }

        public static object GetSession(this ActionExecutingContext filterContext, string sessionName)
        {
            if (filterContext.HttpContext != null && filterContext.HttpContext.Session != null)
            {
                return filterContext.HttpContext.Session[sessionName];
            }

            return null;
        }

        public static T GetSession<T>(this ActionExecutingContext filterContext, string sessionName)
        {
            var value = filterContext.GetSession(sessionName);

            if (value == null)
                return default(T);

            return (T) value;
        }
    }
}