using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zer.Framework.Export;
using Zer.Services.Company.Dto;
using Zer.Services.LngAllowance;
using Zer.Services.LngAllowance.Dto;

namespace com.gyt.ms.Controllers
{
    public class LngAllowanceController : BaseController
    {
        private readonly ILngAllowanceService _lngAllowanceService;

        public LngAllowanceController(ILngAllowanceService lngAllowanceService)
        {
            // TODO: 功能未完成
            _lngAllowanceService = lngAllowanceService;
        }

        public LngAllowanceController()
        {
            
        }

        // GET: LngAllowance
        public ActionResult Index()
        {
            // TODO:Unit Test
            var dto = _lngAllowanceService.GetAll();
            return View(dto);
        }

        public FileResult Export(int[] idList = null)
        {
            // TODO:Unit Test
            var list = _lngAllowanceService.GetList(idList);
            
            return ExportCsv(list.GetBuffer(), "LNG补贴信息");
        }
    }
}