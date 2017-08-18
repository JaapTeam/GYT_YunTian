using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zer.Entities;
using Zer.Framework.Exception;
using Zer.Framework.Mvc.Logs.Attributes;

namespace com.gyt.ms.Controllers
{
    public class HomeController : BaseController
    {
        [UserActionLog("加载首页",ActionType.查询)]
        public ActionResult Index()
        {
            ViewBag.ActiveId = 14;
            return View();
        }

        [UnValidateLogin]
        [UnLog]
        public ActionResult Login()
        {
            return View();
        }

        ////public ActionResult About()
        ////{
        ////    throw new CustomException("define a new exception!",
        ////        new Dictionary<string, string>()
        ////        {
        ////            {"username","paul"}
        ////        });
            
        ////}
    }
}