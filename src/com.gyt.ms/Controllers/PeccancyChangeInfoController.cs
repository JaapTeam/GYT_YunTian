using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zer.AppServices;
using Zer.Entities;
using Zer.Framework.Export;
using Zer.Framework.Extensions;
using Zer.Framework.Mvc.Logs;
using Zer.Framework.Mvc.Logs.Attributes;
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
        [UserActionLog("超载超限已整改记录查询", ActionType.查询)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PeccancySearchDto searchDto)
        {
            // todo:这样做不行呀。只显示已整改信息？
            ViewBag.Result = _peccancyRecrodService.GetList(searchDto).Where(x => x.Status == Status.已整改).ToList();
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [UserActionLog("超载超限已整改记录查询", ActionType.查询)]
        [ValidateAntiForgeryToken]
        public ActionResult Search(PeccancySearchDto searchDto)
        {
            var truckList = _truckInfoService.GetAll();
            var companyList = _companyService.GetAll();

            ViewBag.TruckList = truckList;
            ViewBag.CompanyList = companyList;
            ViewBag.SearchDto = searchDto;

            // todo:这样做不行呀。只显示已整改信息？
            searchDto.Status = Status.已整改;
            ViewBag.Result = _peccancyRecrodService.GetList(searchDto);

            return View("Index");
        }

        [UserActionLog("超载超限记录导出", ActionType.查询)]
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