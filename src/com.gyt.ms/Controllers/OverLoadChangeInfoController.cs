using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.gyt.ms.Controllers
{
    public class OverLoadChangeInfoController : BaseController
    {
        // GET: OverLoadChangeInfo
        public ActionResult Index(int activeId=0)
        {
            ViewBag.ActiveId = activeId;
            return View();
        }
    }
}