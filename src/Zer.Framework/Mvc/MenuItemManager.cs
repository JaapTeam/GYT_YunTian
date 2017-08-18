using System.Collections.Generic;
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

        private void InitMenuItems()
        {
            #region ����ͨҵ�����

            MenuItem businessHandle = new MenuItem();
            businessHandle.Id = 0;
            businessHandle.TextInfo = "����ͨҵ�����";
            businessHandle.Icon = "icon-briefcase";

            var businessHandleChild = new MenuItem();
            businessHandleChild.Id = 1;
            businessHandleChild.ActionName = "Index";
            businessHandleChild.ControllerName = "BusinessHandle";
            businessHandleChild.TextInfo = "����ͨҵ�����";
            businessHandleChild.IsCurrentPage = false;
            businessHandleChild.Icon = "icon-briefcase";

            businessHandle.ChildItems = new List<MenuItem>();
            businessHandle.ChildItems.Add(businessHandleChild);
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

            MenuItem gtyHandleRecrod = new MenuItem();
            gtyHandleRecrod.Id = 4;
            gtyHandleRecrod.ActionName = "Index";
            gtyHandleRecrod.ControllerName = "GYTInfoRecrod";
            gtyHandleRecrod.TextInfo = "����ͨ����̨��";
            gtyHandleRecrod.IsCurrentPage = false;
            gtyHandleRecrod.Icon = "icon-file-alt";

            gytDataManage.ChildItems = new List<MenuItem>();
            gytDataManage.ChildItems.Add(gytInfo);
            gytDataManage.ChildItems.Add(gtyHandleRecrod);
            MenuItems.Add(gytDataManage);

            #endregion

            #region ���س������ݹ���

            MenuItem overLoadDataManage = new MenuItem();
            overLoadDataManage.Id = 5;
            overLoadDataManage.TextInfo = "���س������ݹ���";
            overLoadDataManage
                .Icon = "icon-credit-card";

            MenuItem overLoadRecrod = new MenuItem();
            overLoadRecrod.Id = 6;
            overLoadRecrod.ActionName = "Index";
            overLoadRecrod.ControllerName = "PeccancyRecrod";
            overLoadRecrod.TextInfo = "���س�����Ϣ���ݿ�";
            overLoadRecrod.IsCurrentPage = false;
            overLoadRecrod.Icon = "icon-file-alt";

            MenuItem overLoadChange = new MenuItem();
            overLoadChange.Id = 7;
            overLoadChange.ActionName = "Index";
            overLoadChange.ControllerName = "PeccancyChangeInfo";
            overLoadChange.TextInfo = "���س������������ݿ�";
            overLoadChange.IsCurrentPage = false;
            overLoadChange.Icon = "icon-file-alt";

            overLoadDataManage.ChildItems = new List<MenuItem>();
            overLoadDataManage.ChildItems.Add(overLoadRecrod);
            overLoadDataManage.ChildItems.Add(overLoadChange);
            MenuItems.Add(overLoadDataManage);

            #endregion

            #region LNG������Ϣ���ݿ�

            MenuItem lgnInfo = new MenuItem();
            lgnInfo.Id = 8;
            lgnInfo.TextInfo = "LNG������Ϣ���ݿ�";
            lgnInfo.Icon = "icon-credit-card";

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
            logInfo.Icon = "icon-file-alt";

            MenuItem accountManage = new MenuItem();
            accountManage.Id = 12;
            accountManage.ActionName = "AccountManage";
            accountManage.ControllerName = "User";
            accountManage.TextInfo = "�˻�����";
            accountManage.IsCurrentPage = false;
            accountManage.Icon = "icon-file-alt";

            MenuItem changPassword = new MenuItem();
            changPassword.Id = 13;
            changPassword.ActionName = "ChangePassword";
            changPassword.ControllerName = "User";
            changPassword.TextInfo = "�޸�����";
            changPassword.IsCurrentPage = false;
            changPassword.Icon = "icon-file-alt";

            systemManage.ChildItems = new List<MenuItem>();
            systemManage.ChildItems.Add(logInfo);
            systemManage.ChildItems.Add(accountManage);
            systemManage.ChildItems.Add(changPassword);
            MenuItems.Add(systemManage);

            #endregion

        }
    }
}