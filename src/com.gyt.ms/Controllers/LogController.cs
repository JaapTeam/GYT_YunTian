using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zer.AppServices;
using Zer.Framework.Export;
using Zer.GytDto;
using Zer.Services;

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
        public ActionResult Index()
        {
            ViewBag.ActiveId = 11;
            ViewBag.Result =
                _logInfoService.GetAll()
                    .Where(x => x.CreateTime >= DateTime.Now.AddDays(-7) && x.CreateTime <= DateTime.Now)
                    .ToList();
            //ViewBag.Result = new List<LogsDto>()
            //{
            //    new LogsDto()
            //    {
            //        Id = 1,
            //        ActionType = ActionType.Add,
            //        ActionModel = "用户管理",
            //        Content = "sss~jj~Jj~jj~Jj",
            //        CreateTime = DateTime.Now,
            //        IP = "192.168.1.1",
            //        MAC = "00-23-5A-15-99-42",
            //        UserId = 0001,
            //        DisplayName = "张三"
            //    },
            //    new LogsDto()
            //    {
            //        Id = 2,
            //        ActionType = ActionType.Add,
            //        ActionModel = "用户管理",
            //        Content = "sss~jj~Jj~jj~Jj",
            //        CreateTime = DateTime.Now,
            //        IP = "192.168.1.1",
            //        MAC = "00-23-5A-15-99-42",
            //        UserId = 0001,
            //        DisplayName = "张三"
            //    },
            //    new LogsDto()
            //    {
            //        Id = 3,
            //        ActionType = ActionType.Add,
            //        ActionModel = "用户管理",
            //        Content = "sss~jj~Jj~jj~Jj",
            //        CreateTime = DateTime.Now,
            //        IP = "192.168.1.1",
            //        MAC = "00-23-5A-15-99-42",
            //        UserId = 0001,
            //        DisplayName = "张三"
            //    },
            //    new LogsDto()
            //    {
            //        Id = 4,
            //        ActionType = ActionType.Add,
            //        ActionModel = "用户管理",
            //        Content = "sss~jj~Jj~jj~Jj",
            //        CreateTime = DateTime.Now,
            //        IP = "192.168.1.1",
            //        MAC = "00-23-5A-15-99-42",
            //        UserId = 0001,
            //        DisplayName = "张三"
            //    },
            //};

            return View();
        }

        public ActionResult LogInfo(int logId)
        {
            ViewBag.ActiveId = 11;
            ViewBag.LogInfo = new LogsDto()
            {
                Id = 4,
                ActionType = ActionType.Add,
                ActionModel = "用户管理",
                Content = "sss~jj~Jj~jj~Jj",
                CreateTime = DateTime.Now,
                IP = "192.168.1.1",
                MAC = "00-23-5A-15-99-42",
                UserId = 0001,
                DisplayName = "张三"
            };
            return View();
        }

        public FileResult Export(int[] ids)
        {
            var list = _logInfoService.GetListByIds(ids);

            return ExportCsv(list.GetBuffer(),"日志记录");
        }
    }
}