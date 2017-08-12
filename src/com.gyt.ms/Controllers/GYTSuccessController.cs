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
using Zer.GytDto.SearchFilters;

namespace com.gyt.ms.Controllers
{
    public class GYTSuccessController : BaseController
    {
        private readonly IGYTInfoService _gytInfoService;
        private readonly ITruckInfoService _truckInfoService;
        private readonly ICompanyService _companyService;

        public GYTSuccessController(IGYTInfoService gytInfoService, ITruckInfoService truckInfoService, ICompanyService companyService)
        {
            _gytInfoService = gytInfoService;
            _truckInfoService = truckInfoService;
            _companyService = companyService;
        }

        // GET: GYTSuccess
        public ActionResult Index(int activeId=0)
        {
            ViewBag.ActiveId = activeId;
            ViewBag.TruckList = _truckInfoService.GetAll().ToList();
            ViewBag.CompanyList = _companyService.GetAll().ToList();
            ViewBag.Result = _gytInfoService.GetAll().ToList();
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Search(GYTInfoSearchDto searchDto, int activeId = 7)
        {
            ViewBag.ActiveId = activeId;
            var truckList = _truckInfoService.GetAll();
            var companyList = _companyService.GetAll();

            ViewBag.TruckList = truckList;
            ViewBag.CompanyList = companyList;
            ViewBag.SearchDto = searchDto;

            ViewBag.Result = _gytInfoService.GetList(searchDto);

            return View("Index");
        }

        public FileResult Export(string exportCode = "")
        {
            List<GYTInfoDto> exportList = new List<GYTInfoDto>();

            if (exportCode.IsNullOrEmpty())
            {
                return null;
            }

            if (exportCode.ToLower() == "all")
            {
                exportList = _gytInfoService.GetAll().ToList();
            }
            else
            {
                exportList = GetValueFromSession<List<GYTInfoDto>>(exportCode);
            }

            return exportList == null ? null : ExportCsv(exportList.GetBuffer(), string.Format("港运通办理记录{0:yyyyMMddhhmmssfff}", DateTime.Now));
        }
    }
}