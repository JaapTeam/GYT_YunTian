using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.gyt.ms.Controllers
{
    public class BusinessHandleController : BaseController
    {
        // GET: BusinessHandle
        public ActionResult Index()
        {
            ViewBag.ActiveId = 1;
            return View();
        }
    }
}