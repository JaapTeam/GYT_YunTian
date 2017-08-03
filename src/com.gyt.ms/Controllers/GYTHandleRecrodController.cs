using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.gyt.ms.Controllers
{
    public class GYTHandleRecrodController : BaseController
    {
        // GET: GYTSuccessInfo
        public ActionResult Index(int id=0)
        {
            ViewBag.ActiveId = 4;
            return View();
        }
    }
}