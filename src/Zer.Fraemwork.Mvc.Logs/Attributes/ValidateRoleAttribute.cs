using System;
using System.Linq;
using System.Web.Mvc;
using Zer.Entities;
using Zer.Framework.Exception;
using Zer.GytDto;
using Zer.GytDto.Users;
using ActionType = Zer.Entities.ActionType;

namespace Zer.Framework.Mvc.Logs.Attributes
{
    public class ValidateRoleAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.GetActionAttribute<UnValidateLoginAttribute>().Any())
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            var adminRoleAttributes = filterContext.GetActionAttribute<AdminRoleAttribute>();
            var userInfo = filterContext.GetSession<UserInfoDto>("UserInfo");

            if (adminRoleAttributes.Any() && userInfo.Role != RoleLevel.Administrator)
            {
                UserActionLogger.Instance.Info(new LogInfoDto
                {
                    ActionModel = string.Format("{0}/{1}",
                        filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                        filterContext.ActionDescriptor.ActionName),
                    ActionType = ActionType.����״̬,
                    Content = "ԽȨִ��Σ�ղ���",
                    DisplayName = userInfo.DisplayName,
                    UserId = userInfo.UserId,
                    //CreateTime = DateTime.Now
                });

                throw new CustomException("��û��Ȩ��ִ�д˲���");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}