using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.gyt.ms.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.ActiveId = 14;
            return View();
        }

        public ActionResult Error(string msg)
        {
            ViewBag.ActiveId = 0;
            ViewBag.ErrorMessage = msg;
            return View("Error");
        }

        public ActionResult About()
        {
            throw new Exception("define a new exception!");
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}