using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Zer.GytDto.Users;

namespace Zer.Framework.Mvc.Logs.Attributes
{
    public class ValidateLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ActionDescriptor.GetCustomAttributes(false).Any(x => x is UnValidateLoginAttribute))
            {
                var userInfo = filterContext.Controller.ControllerContext.HttpContext.Session["UserInfo"];

                if (userInfo == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        Controller = "Home",
                        Action = "Login"
                    }));
                    return;
                }

                filterContext.Controller.ViewBag.UserInfo = userInfo;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}