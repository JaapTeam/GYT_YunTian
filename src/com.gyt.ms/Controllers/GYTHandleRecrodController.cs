using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zer.Entities;
using Zer.Framework.Mvc.Logs.Attributes;

namespace com.gyt.ms.Controllers
{
    public class GYTHandleRecrodController : BaseController
    {
        // GET: GYTSuccessInfo
        [UserActionLog("港运通办理信息数据库", ActionType.查询)]
        public ActionResult Index(int id=0)
        {
            ViewBag.ActiveId = 4;
            return View();
        }
    }
}