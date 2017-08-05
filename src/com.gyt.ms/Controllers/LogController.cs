using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zer.AppServices;
using Zer.Framework.Export;
using Zer.GytDto;

namespace com.gyt.ms.Controllers
{
    public class LogController : BaseController
    {
        private readonly ILogInfoService _logInfoService;

        public LogController(ILogInfoService logInfoService)
        {
            _logInfoService = logInfoService;
        }

        public LogController()
        {
        }

        // GET: Log
        public ActionResult Index(int activeId=0)
        {
            ViewBag.ActiveId = activeId;
            ViewBag.Result =
                _logInfoService.GetAll()
                    .Where(x => x.CreateTime >= DateTime.Now.AddDays(-7) && x.CreateTime <= DateTime.Now)
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
    }
}