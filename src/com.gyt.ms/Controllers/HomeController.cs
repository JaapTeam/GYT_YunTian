using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zer.Framework.Exception;

namespace com.gyt.ms.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.ActiveId = 14;
            return View();
        }

        public ActionResult About()
        {
            throw new CustomException("define a new exception!",
                new Dictionary<string, string>()
                {
                    {"username","paul"}
                });
            
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}