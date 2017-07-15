using System.Web.Mvc;
using Zer.Framework.Extensions;
using Zer.Framework.Mvc;

namespace com.gyt.ms.Tests.Controllers
{
    public class JsonHelper : Controller
    {

        public JsonResult SuccessExpected()
        {
            return SuccessExpected("");
        }

        public JsonResult SuccessExpected(string msg)
        {
            return SuccessExpected("", msg);
        }

        public JsonResult SuccessExpected(object data, string msg = "")
        {
            return Json(new { C = ResultCode.Success.ToInt(), msg = msg, data }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FailExpected(string msg = "")
        {
            return Json(new { C = ResultCode.Fail.ToInt(), msg, data = "" }, JsonRequestBehavior.AllowGet);
        }
    }
}