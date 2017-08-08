using System;
using System.Web.Mvc;

namespace Zer.Framework.Mvc.Logs.Attributes
{
    public class CustomExceptionAttribute:FilterAttribute,IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            
        }
    }
}