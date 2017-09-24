using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Zer.Entities;
using Zer.GytDto.Users;

namespace Zer.Framework.Mvc.Logs.Attributes
{
    public class ValidateLoginAttribute : ValidateRoleAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if (filterContext.HttpContext.Request.Url.ToString().Contains("http://localhost:"))
            //{
            //    filterContext.Controller.ControllerContext.HttpContext.Session["UserInfo"] = new UserInfoDto()
            //    {
            //        DisplayName = "≤‚ ‘”√ªß",
            //        UserId = -1,
            //        UserName = "local_testuser",
            //        Role = RoleLevel.Administrator
            //    };
            //}

            if (!filterContext.GetActionAttribute<UnValidateLoginAttribute>().Any())
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