using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Zer.Framework.Dto;

namespace Zer.Framework.Mvc
{
    public class MenuItemManager
    {
        public List<MenuItem> MenuItems { get; private set; }

        public MenuItemManager()
        {
            MenuItems = new List<MenuItem>();
            InitMenuItems();
        }

        public MenuItem GetId(string controllerName, string actionName)
        {
            var menuitem = this.MenuItems
                .SelectMany(x => x.ChildItems)
                 .FirstOrDefault(x =>
                     String.Equals(x.ActionName, actionName, StringComparison.CurrentCultureIgnoreCase) &&
                     String.Equals(x.ControllerName, controllerName, StringComparison.CurrentCultureIgnoreCase));

            return menuitem ?? new MenuItem()
            {
                ActionName = "index",
                ControllerName = "home",
                ChildItems = new List<MenuItem>(),
                Icon = "icon-home",
                Id = 1000,
                TextInfo = "首页"
            };
        }

        private void InitMenuItems()
        {
            MenuItems.Add(new MenuItem()
            {
                ActionName = "index",
                ControllerName = "home",
                ChildItems =  new List<MenuItem>(),
                Icon = "icon-home",
                Id = 1000,
                TextInfo = "首页"
            });

            #region 港运通业务办理

            MenuItem businessHandle = new MenuItem();
            businessHandle.Id = 888;
            businessHandle.TextInfo = "港运通业务办理";
            businessHandle.Icon = "icon-briefcase";

            businessHandle.ChildItems.Add(new MenuItem()
            {
                Id = 101,
                ActionName = "Gas",
                ControllerName = "BusinessHandle",
                TextInfo="天然气车辆业务",
                IsCurrentPage = false,
                Icon = "icon-briefcase"
            });

            businessHandle.ChildItems.Add(new MenuItem()
            {
                Id = 102,
                ActionName = "New",
                ControllerName = "BusinessHandle",
                TextInfo = "以旧换新业务",
                IsCurrentPage = false,
                Icon = "icon-briefcase"
            });

            businessHandle.ChildItems.Add(new MenuItem()
            {
                Id = 103,
                ActionName = "Transfer",
                ControllerName = "BusinessHandle",
                TextInfo = "车辆过户业务",
                IsCurrentPage = false,
                Icon = "icon-briefcase"
            });

            MenuItems.Add(businessHandle);

            #endregion

            #region 港运通数据管理

            MenuItem gytDataManage = new MenuItem();
            gytDataManage.Id = 2;
            gytDataManage.TextInfo = "港运通数据管理";
            gytDataManage.Icon = "icon-credit-card";

            MenuItem gytInfo = new MenuItem();
            gytInfo.Id = 3;
            gytInfo.ActionName = "Index";
            gytInfo.ControllerName = "GYTInfo";
            gytInfo.TextInfo = "港运通信息数据库";
            gytInfo.IsCurrentPage = false;
            gytInfo.Icon = "icon-file-alt";

            gytDataManage.ChildItems = new List<MenuItem>();
            gytDataManage.ChildItems.Add(gytInfo);
            MenuItems.Add(gytDataManage);

            #endregion

            #region 超载超限数据管理

            MenuItem overLoadDataManage = new MenuItem();
            overLoadDataManage.Id = 5;
            overLoadDataManage.TextInfo = "超载超限数据管理";
            overLoadDataManage
                .Icon = "icon-truck";

            MenuItem overLoadRecrod = new MenuItem();
            overLoadRecrod.Id = 6;
            overLoadRecrod.ActionName = "Index";
            overLoadRecrod.ControllerName = "PeccancyRecrod";
            overLoadRecrod.TextInfo = "超载超限信息数据库";
            overLoadRecrod.IsCurrentPage = false;
            overLoadRecrod.Icon = "icon-align-left";

            MenuItem overLoadChange = new MenuItem();
            overLoadChange.Id = 7;
            overLoadChange.ActionName = "Index";
            overLoadChange.ControllerName = "PeccancyChangeInfo";
            overLoadChange.TextInfo = "超载超限已整改数据库";
            overLoadChange.IsCurrentPage = false;
            overLoadChange.Icon = "icon-file-alt";

            overLoadDataManage.ChildItems = new List<MenuItem>();
            overLoadDataManage.ChildItems.Add(overLoadRecrod);
            overLoadDataManage.ChildItems.Add(overLoadChange);
            MenuItems.Add(overLoadDataManage);

            #endregion

            #region LNG补贴信息数据库

            MenuItem lgnInfo = new MenuItem();
            lgnInfo.Id = 8;
            lgnInfo.TextInfo = "LNG补贴信息数据库";
            lgnInfo.Icon = "icon-ambulance";

            MenuItem lgnInfoChild = new MenuItem();
            lgnInfoChild.Id = 9;
            lgnInfoChild.ActionName = "Index";
            lgnInfoChild.ControllerName = "LngAllowance";
            lgnInfoChild.TextInfo = "LNG补贴信息数据库";
            lgnInfoChild.IsCurrentPage = false;
            lgnInfoChild.Icon = "icon-file-alt";

            lgnInfo.ChildItems = new List<MenuItem>();
            lgnInfo.ChildItems.Add(lgnInfoChild);
            MenuItems.Add(lgnInfo);

            #endregion

            #region  系统管理

            MenuItem systemManage = new MenuItem();
            systemManage.Id = 10;
            systemManage.TextInfo = "系统管理";
            systemManage.Icon = "icon-cogs";

            MenuItem logInfo = new MenuItem();
            logInfo.Id = 11;
            logInfo.ActionName = "Index";
            logInfo.ControllerName = "LogInfo";
            logInfo.TextInfo = "日志管理";
            logInfo.IsCurrentPage = false;
            logInfo.Icon = "icon-eye-open";

            MenuItem accountManage = new MenuItem();
            accountManage.Id = 12;
            accountManage.ActionName = "AccountManage";
            accountManage.ControllerName = "User";
            accountManage.TextInfo = "账户管理";
            accountManage.IsCurrentPage = false;
            accountManage.Icon = "icon-user";

            MenuItem changPassword = new MenuItem();
            changPassword.Id = 13;
            changPassword.ActionName = "ChangePassword";
            changPassword.ControllerName = "User";
            changPassword.TextInfo = "修改密码";
            changPassword.IsCurrentPage = false;
            changPassword.Icon = " icon-key";

            systemManage.ChildItems = new List<MenuItem>();
            systemManage.ChildItems.Add(logInfo);
            systemManage.ChildItems.Add(accountManage);
            systemManage.ChildItems.Add(changPassword);
            MenuItems.Add(systemManage);

            #endregion

        }
    }
}