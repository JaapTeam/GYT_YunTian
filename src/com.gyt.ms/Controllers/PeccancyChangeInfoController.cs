using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zer.AppServices;
using Zer.Entities;
using Zer.Framework.Export;

namespace com.gyt.ms.Controllers
{
    public class PeccancyChangeInfoController : BaseController
    {
        private readonly IPeccancyRecrodService _overloadRecrodService;
        private readonly ITruckInfoService _truckInfoService;
        private readonly ICompanyService _companyService;

        public PeccancyChangeInfoController(IPeccancyRecrodService overloadRecrodService, ITruckInfoService truckInfoService, ICompanyService companyService)
        {
            _overloadRecrodService = overloadRecrodService;
            _truckInfoService = truckInfoService;
            _companyService = companyService;
        }

        // GET: OverLoadChangeInfo
        public ActionResult Index(int activeId=0)
        {
            ViewBag.ActiveId = activeId;
            ViewBag.TruckList = _truckInfoService.GetAll().ToList();
            ViewBag.CompanyList = _companyService.GetAll().ToList();
            ViewBag.Result = _overloadRecrodService.GetAll().Where(x=>x.Status==Status.已整改).ToList();
            return View();
        }

        public FileResult Export(int[] ids)
        {
            if (ids.Length <= 0)
            {
                RedirectToAction("index", "Error", "请选择需要导出的记录！");
            }

            var result = _overloadRecrodService.GetListByIds(ids);

            if (result.Count <= 0)
            {
                RedirectToAction("index", "Error", "未查询到相关数据！");
            }

            return ExportCsv(result.GetBuffer(), "违法记录");
        }
    }
}