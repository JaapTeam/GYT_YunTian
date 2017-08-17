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
    public class PeccancyChangeInfoController : BaseController
    {
        private readonly IPeccancyRecrodService _peccancyRecrodService;
        private readonly ITruckInfoService _truckInfoService;
        private readonly ICompanyService _companyService;

        public PeccancyChangeInfoController(IPeccancyRecrodService overloadRecrodService, ITruckInfoService truckInfoService, ICompanyService companyService)
        {
            _peccancyRecrodService = overloadRecrodService;
            _truckInfoService = truckInfoService;
            _companyService = companyService;
        }

        // GET: OverLoadChangeInfo
        public ActionResult Index(PeccancySearchDto searchDto, int activeId = 9)
        {
            ViewBag.ActiveId = activeId;
            ViewBag.Result = _peccancyRecrodService.GetList(searchDto).Where(x => x.Status == Status.已整改).ToList();
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Search(PeccancySearchDto searchDto, int activeId = 7)
        {
            ViewBag.ActiveId = activeId;
            var truckList = _truckInfoService.GetAll();
            var companyList = _companyService.GetAll();

            ViewBag.TruckList = truckList;
            ViewBag.CompanyList = companyList;
            ViewBag.SearchDto = searchDto;

            searchDto.Status = Status.已整改;
            ViewBag.Result = _peccancyRecrodService.GetList(searchDto);

            return View("Index");
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
                exportList = _peccancyRecrodService.GetAll().Where(x => x.Status == Status.已整改).ToList();
            }
            else
            {
                exportList = GetValueFromSession<List<PeccancyRecrodDto>>(exportCode);
            }

            return exportList == null ? null : ExportCsv(exportList.GetBuffer(), string.Format("超载超限整改记录{0:yyyyMMddhhmmssfff}", DateTime.Now));
        }
    }
}