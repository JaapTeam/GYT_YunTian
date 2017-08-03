using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.gyt.ms.Controllers
{
    public class GYTInfoController : BaseController
    {
        // GET: GYTInfo
        public ActionResult Index()
        {
            ViewBag.ActiveId = 3;
            return View();
        }
    }
}