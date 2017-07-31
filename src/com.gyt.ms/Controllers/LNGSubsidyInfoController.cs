using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.gyt.ms.Controllers
{
    public class LNGSubsidyInfoController : BaseController
    {
        // GET: LNGSubsidyInfo
        public ActionResult Index()
        {
            ViewBag.ActiveId = 9;
            return View();
        }
    }
}