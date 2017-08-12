using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zer.AppServices;
using Zer.Entities;
using Zer.Framework.Export;
using Zer.Framework.Extensions;
using Zer.GytDto;

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

        public FileResult Export(string exportCode="")
        {
            List<PeccancyRecrodDto> exportList = new List<PeccancyRecrodDto>();

            if (exportCode.IsNullOrEmpty())
            {
                return null;
            }

            if (exportCode.ToLower() == "all")
            {
                exportList = _overloadRecrodService.GetAll().Where(x => x.Status == Status.已整改).ToList();
            }
            else
            {
                exportList = GetValueFromSession<List<PeccancyRecrodDto>>(exportCode);
            }

            return exportList == null ? null : ExportCsv(exportList.GetBuffer(), string.Format("超载超限整改记录{0:yyyyMMddhhmmssfff}", DateTime.Now));
        }
    }
}