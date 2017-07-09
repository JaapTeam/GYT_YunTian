using System.Web;
using System.Web.Mvc;
using Abp.Runtime.Caching;
using Abp.Web.Mvc.Authorization;
using LearningForAbp.Tasks;
using LearningForAbp.Tasks.Dtos;
using LearningForAbp.Users;
using LearningForAbp.Web.Models.Tasks;

namespace LearningForAbp.Web.Controllers
{
    [AbpMvcAuthorize]
    public class TasksController : Controller
    {
        private readonly ITaskAppService _taskAppService;
        private readonly IUserAppService _userAppService;
        private readonly ICacheManager _cacheManager;

        public TasksController(
            ITaskAppService taskAppService,
            IUserAppService userAppService,
            ICacheManager cacheManager)
        {
            _taskAppService = taskAppService;
            _userAppService = userAppService;
            _cacheManager = cacheManager;
        }

        // GET: Tasks
        public ActionResult Index(GetTasksInput input)
        {
            var output = _taskAppService.GetTasks(input);
            var model = new IndexViewModel(output.Tasks)
            {
                SelectedTaskState = input.State
            };
            return View(model);
        }

        public PartialViewResult GetList(GetTasksInput input)
        {
            var output = _taskAppService.GetTasks(input);
            return PartialView("_List", output.Tasks);
        }
    }
}