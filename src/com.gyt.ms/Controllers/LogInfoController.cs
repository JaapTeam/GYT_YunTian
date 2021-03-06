﻿using System.Linq;
using System.Web.Mvc;
using Zer.AppServices;
using Zer.Entities;
using Zer.Framework.Export;
using Zer.Framework.Mvc.Logs.Attributes;
using Zer.GytDto.SearchFilters;
using Zer.GytDto.Users;

namespace com.gyt.ms.Controllers
{
    [RoutePrefix("log")]
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
        //[UserActionLog("查询日志记录", ActionType.查询)]
        [Route("")]
        public ActionResult Index(LogInfoSearchDto filter)
        {
            ViewBag.Result = _logInfoService.GetList(filter, GetValueFromSession<UserInfoDto>("UserInfo"));
            ViewBag.Filter = filter;
            return View();
        }

        [UserActionLog("查看日志记录详情", ActionType.查询)]
        [Route("detail/{logId}")]
        public ActionResult LogInfo(int logId)
        {
            ViewBag.LogInfo = _logInfoService.GetById(logId);
            return View();
        }

        [UserActionLog("导出日志记录", ActionType.查询)]
        [Route("export")]
        public FileResult Export()
        {
            var filter = new LogInfoSearchDto()
            {
                PageIndex = 1,
                PageSize = int.MaxValue
            };

           var list = _logInfoService.GetList(filter, GetValueFromSession<UserInfoDto>("UserInfo"));

            return ExportCsv(list.GetBuffer(),"日志记录");
        }

        [UserActionLog("查询当前用户日志记录", ActionType.查询)]
        [Route("u/l")]
        public ActionResult UserLogInfo()
        {
            var userInfoDto = GetValueFromSession<UserInfoDto>("UserInfo");
            ViewBag.Result = _logInfoService.GetListByUserId(userInfoDto.UserId, GetValueFromSession<UserInfoDto>("UserInfo"));
            return View();
        }
    }
}