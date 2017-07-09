using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using LearningForAbp.Authorization;
using LearningForAbp.MultiTenancy;

namespace LearningForAbp.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Tenants)]
    public class TenantsController : LearningForAbpControllerBase
    {
        private readonly ITenantAppService _tenantAppService;

        public TenantsController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }

        public ActionResult Index()
        {
            var output = _tenantAppService.GetTenants();
            return View(output);
        }
    }
}