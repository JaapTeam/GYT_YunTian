using System.Web.Mvc;

namespace LearningForAbp.Web.Controllers
{
    public class AboutController : LearningForAbpControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}