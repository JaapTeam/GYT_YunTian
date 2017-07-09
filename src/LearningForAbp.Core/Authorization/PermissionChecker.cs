using Abp.Authorization;
using LearningForAbp.Authorization.Roles;
using LearningForAbp.MultiTenancy;
using LearningForAbp.Users;

namespace LearningForAbp.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
