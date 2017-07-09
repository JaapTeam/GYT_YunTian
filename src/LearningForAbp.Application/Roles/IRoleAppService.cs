using System.Threading.Tasks;
using Abp.Application.Services;
using LearningForAbp.Roles.Dto;

namespace LearningForAbp.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
