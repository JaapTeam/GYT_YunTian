using System;
using System.Collections.Generic;
using System.Linq;
using Zer.Entities;
using Zer.Framework.Dto;
using Zer.GytDto.Users;

namespace Zer.Framework.Mvc.Logs
{
    public class MenuItemManager
    {
        public List<MenuItem> MenuItems { get; private set; }

        private readonly UserInfoDto _userInfo;

        public MenuItemManager(UserInfoDto userInfo)
        {
            _userInfo = userInfo;
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
                TextInfo = "��ҳ"
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
                TextInfo = "��ҳ"
            });

            #region ����ͨҵ�����

            MenuItem businessHandle = new MenuItem();
            businessHandle.Id = 888;
            businessHandle.TextInfo = "����ͨҵ�����";
            businessHandle.Icon = "icon-briefcase";

            businessHandle.ChildItems.Add(new MenuItem()
            {
                Id = 101,
                ActionName = "Gas",
                ControllerName = "BusinessHandle",
                TextInfo="��Ȼ������ҵ��",
                IsCurrentPage = false,
                Icon = "icon-briefcase"
            });

            businessHandle.ChildItems.Add(new MenuItem()
            {
                Id = 102,
                ActionName = "New",
                ControllerName = "BusinessHandle",
                TextInfo = "�Ծɻ���ҵ��",
                IsCurrentPage = false,
                Icon = "icon-briefcase"
            });

            businessHandle.ChildItems.Add(new MenuItem()
            {
                Id = 103,
                ActionName = "Transfer",
                ControllerName = "BusinessHandle",
                TextInfo = "��������ҵ��",
                IsCurrentPage = false,
                Icon = "icon-briefcase"
            });

            MenuItems.Add(businessHandle);

            #endregion

            #region ����ͨ���ݹ���

            MenuItem gytDataManage = new MenuItem();
            gytDataManage.Id = 2;
            gytDataManage.TextInfo = "����ͨ���ݹ���";
            gytDataManage.Icon = "icon-credit-card";

            MenuItem gytInfo = new MenuItem();
            gytInfo.Id = 3;
            gytInfo.ActionName = "Index";
            gytInfo.ControllerName = "GYTInfo";
            gytInfo.TextInfo = "����ͨ��Ϣ���ݿ�";
            gytInfo.IsCurrentPage = false;
            gytInfo.Icon = "icon-file-alt";

            gytDataManage.ChildItems = new List<MenuItem>();
            gytDataManage.ChildItems.Add(gytInfo);
            MenuItems.Add(gytDataManage);

            #endregion

            #region ���س������ݹ���

            MenuItem overLoadDataManage = new MenuItem();
            overLoadDataManage.Id = 5;
            overLoadDataManage.TextInfo = "���س������ݹ���";
            overLoadDataManage
                .Icon = "icon-truck";

            MenuItem peccancyRecrod = new MenuItem();
            peccancyRecrod.Id = 6;
            peccancyRecrod.ActionName = "Index";
            peccancyRecrod.ControllerName = "PeccancyRecrod";
            peccancyRecrod.TextInfo = "���س�����Ϣ���ݿ�";
            peccancyRecrod.IsCurrentPage = false;
            peccancyRecrod.Icon = "icon-align-left";

            MenuItem peccancyWithCompany = new MenuItem();
            peccancyWithCompany.Id = 7;
            peccancyWithCompany.ActionName = "Company";
            peccancyWithCompany.ControllerName = "PeccancyRecrod";
            peccancyWithCompany.TextInfo = "��˾Υ����Ϣ��ѯ";
            peccancyWithCompany.IsCurrentPage = false;
            peccancyWithCompany.Icon = "icon-file-alt";

            overLoadDataManage.ChildItems = new List<MenuItem>();
            overLoadDataManage.ChildItems.Add(peccancyRecrod);
            overLoadDataManage.ChildItems.Add(peccancyWithCompany);
            MenuItems.Add(overLoadDataManage);

            #endregion

            #region LNG������Ϣ���ݿ�

            MenuItem lgnInfo = new MenuItem();
            lgnInfo.Id = 8;
            lgnInfo.TextInfo = "LNG������Ϣ���ݿ�";
            lgnInfo.Icon = "icon-ambulance";

            MenuItem lgnInfoChild = new MenuItem();
            lgnInfoChild.Id = 9;
            lgnInfoChild.ActionName = "Index";
            lgnInfoChild.ControllerName = "LngAllowance";
            lgnInfoChild.TextInfo = "LNG������Ϣ���ݿ�";
            lgnInfoChild.IsCurrentPage = false;
            lgnInfoChild.Icon = "icon-file-alt";

            lgnInfo.ChildItems = new List<MenuItem>();
            lgnInfo.ChildItems.Add(lgnInfoChild);
            MenuItems.Add(lgnInfo);

            #endregion

            #region  ϵͳ����

            MenuItem systemManage = new MenuItem();
            systemManage.Id = 10;
            systemManage.TextInfo = "ϵͳ����";
            systemManage.Icon = "icon-cogs";

            MenuItem logInfo = new MenuItem();
            logInfo.Id = 11;
            logInfo.ActionName = "Index";
            logInfo.ControllerName = "LogInfo";
            logInfo.TextInfo = "��־����";
            logInfo.IsCurrentPage = false;
            logInfo.Icon = "icon-eye-open";

            //if (_userInfo.Role != RoleLevel.User)
            //{
                systemManage.ChildItems.Add(new MenuItem
                                            {
                                                Id = 12,
                                                ActionName = "AccountManage",
                                                ControllerName = "User",
                                                TextInfo = "�˻�����",
                                                IsCurrentPage = false,
                                                Icon = "icon-user"
                                            });
            //}

            MenuItem changPassword = new MenuItem();
            changPassword.Id = 13;
            changPassword.ActionName = "ChangePassword";
            changPassword.ControllerName = "User";
            changPassword.TextInfo = "�޸�����";
            changPassword.IsCurrentPage = false;
            changPassword.Icon = " icon-key";

            systemManage.ChildItems = new List<MenuItem>();
            systemManage.ChildItems.Add(logInfo);
            systemManage.ChildItems.Add(changPassword);
            MenuItems.Add(systemManage);

            #endregion

        }
    }
}