using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.gyt.ms.Controllers
{
    public class LogController : BaseController
    {
        // GET: Log
        public ActionResult Index()
        {
            ViewBag.ActiveId = 11;
            return View();
        }
    }
}