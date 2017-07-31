using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.gyt.ms.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(string message="")
        {
            return View();
        }
    }
}