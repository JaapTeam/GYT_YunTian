using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Zer.Framework.Mvc.Logs.Attributes
{
    public class SetMenuItemAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.ActionName;
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            var menuItemManager = new MenuItemManager();

            var menuItemId = menuItemManager.GetId(controllerName, actionName);
            

            foreach (var item in menuItemManager.MenuItems)
            {
                if (item.ChildItems == null) continue;
                foreach (var child in item.ChildItems)
                {
                    if (child.Id != menuItemId) continue;

                    item.IsCurrentPage = true;
                    child.IsCurrentPage = true;
                }
            }

            filterContext.Controller.ViewBag.MenuItems = menuItemManager.MenuItems;
            filterContext.Controller.ViewBag.ActiveId = menuItemId;
            base.OnActionExecuting(filterContext);
        }
    }
}
