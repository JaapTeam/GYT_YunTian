using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Zer.AppServices;
using Zer.Entities;
using Zer.Framework.Export;
using Zer.Framework.Extensions;
using Zer.GytDto;
using Zer.GytDto.SearchFilters;

namespace com.gyt.ms.Controllers
{
    public class GYTInfoRecrodController : BaseController
    {
        private readonly IGYTInfoService _gytInfoService;
        private readonly ITruckInfoService _truckInfoService;
        private readonly ICompanyService _companyService;

        public GYTInfoRecrodController(IGYTInfoService gytInfoService, ITruckInfoService truckInfoService, ICompanyService companyService)
        {
            _gytInfoService = gytInfoService;
            _truckInfoService = truckInfoService;
            _companyService = companyService;
        }

        // GET: GYTSuccess
        public ActionResult Index(GYTInfoSearchDto searchDto, int activeId = 7)
        {
            ViewBag.ActiveId = activeId;
            ViewBag.SearchDto = searchDto;
            ViewBag.Result = _gytInfoService.GetList(searchDto);
            return View();
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

        public JsonResult Verify(int infoId)
        {
            var gtyInfo = _gytInfoService.GetById(infoId);

            if (gtyInfo.Status == BusinessState.已审核)
            {
                return Fail("这条记录已经审核！");
            }

            gtyInfo = _gytInfoService.Verify(infoId);

            if (gtyInfo.Status != BusinessState.已审核)
            {
                return Fail("审核失败！");
            }

            return Success("审核成功！");
        }
    }
}