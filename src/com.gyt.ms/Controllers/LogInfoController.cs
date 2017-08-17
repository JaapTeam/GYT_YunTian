using System.Linq;
using System.Web.Mvc;
using Zer.AppServices;
using Zer.Framework.Export;

namespace com.gyt.ms.Controllers
{
    public class LogInfoController : BaseController
    {
        private readonly ILogInfoService _logInfoService;

        public LogInfoController(ILogInfoService logInfoService)
        {
            _logInfoService = logInfoService;
        }

        public LogInfoController()
        {
        }

        // GET: Log
        public ActionResult Index(int activeId=0)
        {
            ViewBag.ActiveId = activeId;
            ViewBag.Result =
                _logInfoService.GetAll()
                    .Where(x=>x.UserId==CurrentUser.UserId)
                    .ToList();

            return View();
        }

        public ActionResult LogInfo(int activeId,int logId)
        {
            ViewBag.ActiveId = activeId;
            ViewBag.LogInfo = _logInfoService.GetById(logId);
            return View();
        }

        public FileResult Export(int[] ids)
        {
            var list = _logInfoService.GetListByIds(ids);

            return ExportCsv(list.GetBuffer(),"日志记录");
        }

        public ActionResult UserLogInfo(int userId=0,int activeId=0)
        {
            ViewBag.ActiveId = activeId;
            ViewBag.Result = _logInfoService.GetListByUserId(userId);
            return View();
        }
    }
}