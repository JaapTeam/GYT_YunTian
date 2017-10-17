using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Zer.Framework.Cache;
using Zer.Framework.Exception;

namespace Zer.Framework.Mvc.Logs.Attributes
{
    public class CsrfFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            var context = filterContext.RequestContext.HttpContext;

            var method = context.Request.HttpMethod;
            var allowMethods = new[] {"GET", "POST"};

            if (!allowMethods.Any(x=>x.Equals(method.ToUpper())))
            {
                throw new System.Exception("∑«∑®«Î«Û");
            }

            var host = context.Request.UrlReferrer?.Host;
            var domain = CacheHelper.GetCache("WebHost").ToString();
            
            if (host != null && host.Contains(domain))
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            if(!(context.Request.Url.ToString() == $"http://{domain}:{context.Request.Url.Port}/login"
                 || context.Request.Url.ToString() == $"http://{domain}:{context.Request.Url.Port}/"))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    Controller = "Home",
                    Action = "Login"
                }));
                filterContext.Controller.ViewBag.UserInfo = null;
                return;
            }
        }
    }
}