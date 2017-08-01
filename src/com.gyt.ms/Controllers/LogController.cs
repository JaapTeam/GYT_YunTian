using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zer.Framework.Export;
using Zer.GytDto;
using Zer.Services;

namespace com.gyt.ms.Controllers
{
    public class LogController : BaseController
    {
        //private readonly ILogsService _logsService;

        //public LogController(ILogsService logsService)
        //{
        //    _logsService = logsService;
        //}

        // GET: Log
        public ActionResult Index()
        {
            ViewBag.ActiveId = 11;
            //ViewBag.Result =
            //    _logsService.GetAll()
            //        .Where(x => x.CreateTime >= DateTime.Now.AddDays(-7) && x.CreateTime <= DateTime.Now)
            //        .ToList();

            ViewBag.Result = new List<LogsDto>();
            //{
            //    new LogsDto()
            //    {
            //        Id = 1,
            //        UserId = 00001,
            //        DisplayName = "张三",
            //        ActionModel = "ss",
            //        ActionType = ActionType.Add,
            //        CreateTime = DateTime.Now,
            //        MAC = "2A-33-21-01-HS",
            //        IP = "192.168.8.244",
            //        Content = "sssssssssssssssssss"
            //    }
            //    ,
            //    new LogsDto()
            //    {
            //        Id = 2,
            //        UserId = 00001,
            //        DisplayName = "张三",
            //        ActionModel = "ss",
            //        ActionType = ActionType.Add,
            //        CreateTime = DateTime.Now,
            //        MAC = "2A-33-21-01-HS",
            //        IP = "192.168.8.244",
            //        Content = "sssssssssssssssssss"
            //    },
            //    new LogsDto()
            //    {
            //        Id = 3,
            //        UserId = 00001,
            //        DisplayName = "张三",
            //        ActionModel = "ss",
            //        ActionType = ActionType.Add,
            //        CreateTime = DateTime.Now,
            //        MAC = "2A-33-21-01-HS",
            //        IP = "192.168.8.244",
            //        Content = "sssssssssssssssssss"
            //    }
            //};

            return View();
        }

        public ActionResult LogInfo(int logId)
        {
            ViewBag.ActiveId = 11;
            //ViewBag.LogInfo = _logsService.GetById(logId);
            ViewBag.LogInfo = new LogsDto()
            {
                Id = 3,
                UserId = 00001,
                DisplayName = "张三",
                ActionModel = "ss",
                ActionType = ActionType.Add,
                CreateTime = DateTime.Now,
                MAC = "2A-33-21-01-HS",
                IP = "192.168.8.244",
                Content = "sssssssssssssssssss"
            };
            return View();
        }

        public FileResult Export(int[] ids)
        {
            //var list = _logsService.GetListByIds(ids);

            var list = new List<LogsDto>()
            {
                new LogsDto()
                {
                    Id = 1,
                    UserId = 00001,
                    DisplayName = "张三",
                    ActionModel = "ss",
                    ActionType = ActionType.Add,
                    CreateTime = DateTime.Now,
                    MAC = "2A-33-21-01-HS",
                    IP = "192.168.8.244",
                    Content = "sssssssssssssssssss"
                }
                ,
                new LogsDto()
                {
                    Id = 2,
                    UserId = 00001,
                    DisplayName = "张三",
                    ActionModel = "ss",
                    ActionType = ActionType.Add,
                    CreateTime = DateTime.Now,
                    MAC = "2A-33-21-01-HS",
                    IP = "192.168.8.244",
                    Content = "sssssssssssssssssss"
                },
                new LogsDto()
                {
                    Id = 3,
                    UserId = 00001,
                    DisplayName = "张三",
                    ActionModel = "ss",
                    ActionType = ActionType.Add,
                    CreateTime = DateTime.Now,
                    MAC = "2A-33-21-01-HS",
                    IP = "192.168.8.244",
                    Content = "sssssssssssssssssss"
                }
            };

            return ExportCsv(list.GetBuffer(),"日志记录");
        }
    }
}