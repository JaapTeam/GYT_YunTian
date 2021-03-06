﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Zer.GytDto.Users;

namespace Zer.Framework.Mvc.Logs.Attributes
{
    public class SetMenuItemAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.ActionName;
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            var userInfo = filterContext.HttpContext.Session["UserInfo"] as UserInfoDto;

            if (userInfo == null)
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            var menuItemManager = new MenuItemManager(userInfo);

            var currentMentItem = menuItemManager.GetId(controllerName, actionName);
            

            foreach (var item in menuItemManager.MenuItems)
            {
                if (item.Id == currentMentItem.Id)
                {
                    item.IsCurrentPage = true;
                    break;
                }
                if (item.ChildItems == null) continue;
                foreach (var child in item.ChildItems)
                {
                    if (child.Id != currentMentItem.Id) continue;
                    item.IsCurrentPage = true;
                    child.IsCurrentPage = true;
                    break;
                }
            }

            filterContext.Controller.ViewBag.MenuItems = menuItemManager.MenuItems;
            filterContext.Controller.ViewBag.Title = currentMentItem.TextInfo;
            filterContext.Controller.ViewBag.ActiveId = currentMentItem.Id;
            base.OnActionExecuting(filterContext);
        }
    }
}
