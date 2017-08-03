using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Zer.AppServices;
using Zer.Framework.Export;
using Zer.Services;

namespace com.gyt.ms.Controllers
{
    //ToDo:还有Add功能没做
    public class OverloadRecrodController : BaseController
    {
        private readonly IOverloadRecrodService _overloadRecrodService;

        public OverloadRecrodController()
        {
           
        }

        public OverloadRecrodController(IOverloadRecrodService overloadRecrodService)
        {
            _overloadRecrodService = overloadRecrodService;
        }

        public ActionResult Index()
        {
            ViewBag.ActiveId = 6;
            return View();
        }

        //ToDo:单元测试
        public JsonResult Chang(int id=0)
        {
            if (id == 0)
            {
                return Fail("请选择需要整改的记录！");
            }

            var result = _overloadRecrodService.ChangedById(id);

            if (!result)
            {
                return Fail();
            }

            return Success();
        }

        //ToDo:单元测试
        public FileResult Export(int[] ids)
        {
            if (ids.Length<=0)
            {
                RedirectToAction("index", "Error", "请选择需要导出的记录！");
            }

            var result = _overloadRecrodService.GetListByIds(ids);

            if (result.Count<=0)
            {
                RedirectToAction("index", "Error", "未查询到相关数据！");
            }

            return ExportCsv(result.GetBuffer(), "违法记录");
        }
    }
}