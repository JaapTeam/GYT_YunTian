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
        public ActionResult Index()
        {
            ViewBag.ActiveId = 7;
            return View();
        }
    }
}