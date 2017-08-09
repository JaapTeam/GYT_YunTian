using System;
using System.Web.Mvc;

namespace Zer.Framework.Mvc.Logs.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class NoAuthorizeAttribute : ActionFilterAttribute
    {
        
    }
}