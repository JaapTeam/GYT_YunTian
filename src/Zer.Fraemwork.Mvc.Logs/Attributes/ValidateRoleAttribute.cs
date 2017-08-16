using System.Linq;
using System.Web.Mvc;
using Zer.Entities;
using Zer.Framework.Exception;
using Zer.GytDto;
using Zer.GytDto.Users;
using ActionType = Zer.GytDto.ActionType;

namespace Zer.Framework.Mvc.Logs.Attributes
{
    public class ValidateRoleAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var  userInfo = filterContext.GetSession<UserInfoDto>("UserInfo");

            if (userInfo.Role == RoleLevel.User)
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            var roleAttributes = filterContext.GetActionAttribute<RoleAttribute>();

            if (roleAttributes.All(x => x.RoleLevel != userInfo.Role))
            {
                UserActionLogger.Instance.Info(new LogInfoDto
                {
                    ActionModel = string.Format("{0}/{1}",
                        filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                        filterContext.ActionDescriptor.ActionName),
                        ActionType = ActionType.Change,
                        Content = "ԽȨִ��Σ�ղ���",
                        DisplayName = userInfo.DisplayName,
                        UserId = userInfo.UserId
                });

                throw new CustomException(
                    "��û��Ȩ��ִ�д˲���", "��������",
                    string.Format("{0}/{1}",
                    filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    filterContext.ActionDescriptor.ActionName));
            }

            base.OnActionExecuting(filterContext);
        }
    }
}