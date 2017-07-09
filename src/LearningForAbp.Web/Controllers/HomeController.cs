using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace LearningForAbp.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : LearningForAbpControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}