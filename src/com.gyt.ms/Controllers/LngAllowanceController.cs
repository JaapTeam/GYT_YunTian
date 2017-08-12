using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Castle.Components.DictionaryAdapter;
using Castle.Core.Internal;
using Zer.AppServices;
using Zer.Entities;
using Zer.Framework.Attributes;
using Zer.Framework.Export;
using Zer.Framework.Export.Attributes;
using Zer.Framework.Helpers;
using Zer.Framework.Mvc.Logs.Attributes;
using Zer.GytDto;
using Zer.GytDto.SearchFilters;

namespace com.gyt.ms.Controllers
{
    public class LngAllowanceController : BaseController
    {
        private readonly ILngAllowanceService _lngAllowanceService;
        private readonly ICompanyService _companyService;
        private readonly ITruckInfoService _truckInfoService;

        public LngAllowanceController(
            ILngAllowanceService lngAllowanceService,
            ICompanyService companyService,
            ITruckInfoService truckInfoService)
        {
            _lngAllowanceService = lngAllowanceService;
            _companyService = companyService;
            _truckInfoService = truckInfoService;
        }

        // GET: LngAllowance
        public ActionResult Index(int activeId = 9)
        {
            ViewBag.ActiveId = activeId;

            var truckList = _truckInfoService.GetAll();
            ViewBag.TruckList = truckList;

            var dto = _lngAllowanceService.GetAll();
            return View(dto);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Search(LngAllowanceSearchDto searchDto, int activeId = 9)
        {
            ViewBag.ActiveId = activeId;
            var truckList = _truckInfoService.GetAll();

            ViewBag.TruckList = truckList;
            ViewBag.SearchDto = searchDto;

            var result = _lngAllowanceService.GetList(searchDto);
            return View("Index", result);
        }

        public ActionResult Add(int activeId = 9)
        {
            ViewBag.ActiveId = activeId;
            ViewBag.CompanyList = _companyService.GetAll();
            return View();
        }

        public FileResult Export(string exportCode = "")
        {
            
            if (exportCode.IsNullOrEmpty())
            {
                return null;
            }

            List<LngAllowanceInfoDto> exportList = new List<LngAllowanceInfoDto>();

            if (exportCode.ToLower() == "all")
            {
                exportList = _lngAllowanceService.GetAll();
            }
            else
            {
                exportList = GetValueFromSession<List<LngAllowanceInfoDto>>(exportCode);
            }

            return exportList == null ? null : ExportCsv(exportList.GetBuffer(), string.Format("LNG补贴信息{0:yyyyMMddhhmmssfff}",DateTime.Now));
        }


        //Todo: 建议优化检查检查重复业务逻辑
        [System.Web.Mvc.HttpPost]
        public ActionResult ImportFile(HttpPostedFileBase file)
        {
            if (file != null)
            {
                List<LngAllowanceInfoDto> lngAllowanceInfoDtoList;
                using (StreamReader reader = new StreamReader(file.InputStream, Encoding.Default))
                {
                    lngAllowanceInfoDtoList = Zer.Framework.Import.Import.Read<LngAllowanceInfoDto>(reader, 1);
                }

                if (lngAllowanceInfoDtoList != null)
                {
                    var sessionCode = AppendObjectToSession(lngAllowanceInfoDtoList);
                    ////return SaveLngAllowanceData(lngAllowanceInfoDtoList);
                    return RedirectToAction("SaveLngAllowanceData", "LngAllowance",
                        new {id = sessionCode});
                }
            }

            throw new Exception("文件上传失败，导入失败");
        }
        
        [ReplaceSpecialCharInParameter("-", "_")]
        [GetParameteFromSession("id")]
        [UnLog]
        public ActionResult SaveLngAllowanceData(string id)
        {
            //// 替换气罐号中的 '-',
            //lngAllowanceInfoDtoList = lngAllowanceInfoDtoList.Select(ReplaceUnsafeChar).ToList();

            //// 然后检查导入数据是否包含非法字符
            //lngAllowanceInfoDtoList.ForEach(ValidateHelper.ValidateObjectIsSafe);

            var lngAllowanceInfoDtoList = GetValueFromSession<List<LngAllowanceInfoDto>>(id);

            // 检测数据库中已经存在的重复数据
            var existsLngAllowanceInfoDtoList = lngAllowanceInfoDtoList
                .Where(x => _lngAllowanceService.Exists(x))
                .ToList();

            // 筛选出需要导入的数据
            var mustImportLngAllowanceInfoDtoList =
                lngAllowanceInfoDtoList
                    .Where(x => !existsLngAllowanceInfoDtoList.Select(lng => lng.TruckNo).Contains(x.TruckNo)).ToList();

            // 初始化检测并注册其中的新公司信息
            var companyInfoDtoList = InitCompanyInfoDtoList(mustImportLngAllowanceInfoDtoList);

            var dic = mustImportLngAllowanceInfoDtoList.ToDictionary(x => x.TruckNo, v => v.CompanyId);

            // 初始化检测并注册其中的新车辆信息
            InitTruckInfoDtoList(dic, companyInfoDtoList);

            // 保存LNG补贴信息，并得到保存成功的结果
            var importSuccessList = _lngAllowanceService.AddRange(mustImportLngAllowanceInfoDtoList);

            var importFailedList = mustImportLngAllowanceInfoDtoList.Where(x => importSuccessList.Contains(x))
                .ToList();

            // 展示导入结果
            ViewBag.ActiveId = 9;

            ViewBag.SuccessCode = AppendObjectToSession(importSuccessList);
            ViewBag.FailedCode = AppendObjectToSession(importFailedList);
            ViewBag.ExistedCode = AppendObjectToSession(existsLngAllowanceInfoDtoList);

            ViewBag.SuccessList = importSuccessList;
            ViewBag.FailedList = importFailedList;
            ViewBag.ExistedList = existsLngAllowanceInfoDtoList;
            return View("ImportResult");
        }

        [System.Web.Mvc.HttpPost]
        [ReplaceSpecialCharInParameter("-","_")]
        public JsonResult AddPost(LngAllowanceInfoDto dto)
        {
            //TODO:加特殊字符替换

            CompanyInfoDto companyInfoDto = _companyService.GetById(dto.CompanyId);
            dto.CompanyName = companyInfoDto.CompanyName;

            if (_truckInfoService.Exists(dto.TruckNo))
            {
                _truckInfoService.GetByTruckNo(dto.TruckNo);
            }
            else
            {
                _truckInfoService.Add(new TruckInfoDto()
                {
                    CompanyId = companyInfoDto.Id,
                    CompanyName = companyInfoDto.CompanyName,
                    FrontTruckNo = dto.TruckNo
                });
            }

            _lngAllowanceService.Add(dto);

            return Success();
        }

        private List<CompanyInfoDto> InitCompanyInfoDtoList(List<LngAllowanceInfoDto> lngAllowanceInfoDtoList)
        {
            //var companyNameList = lngAllowanceInfoDtoList.Select(x => x.CompanyName).ToList();
            var improtCompanyInfoDtoList = new List<CompanyInfoDto>();
            foreach (var lngAllowanceInfoDto in lngAllowanceInfoDtoList)
            {
                improtCompanyInfoDtoList.Add(
                    new CompanyInfoDto
                    {
                        CompanyName = lngAllowanceInfoDto.CompanyName
                    }
                );
            }

            // 注册新增公司信息
            var companyInfoDtoList = _companyService.QueryAfterValidateAndRegist(improtCompanyInfoDtoList);

            foreach (var lngAllowanceInfoDto in lngAllowanceInfoDtoList)
            {
                var currentComapnyInfoDto =
                    companyInfoDtoList.Single(x => x.CompanyName == lngAllowanceInfoDto.CompanyName);

                lngAllowanceInfoDto.CompanyId = currentComapnyInfoDto.Id;
            }
            return companyInfoDtoList;
        }

        private void InitTruckInfoDtoList(Dictionary<string, int> dic, List<CompanyInfoDto> companyInfoDtoList)
        {
            var waitForValidateTruckList = new List<TruckInfoDto>();

            foreach (var truckNo in dic.Keys)
            {
                var companyInfo = companyInfoDtoList.Single(x => x.Id == dic[truckNo]);
                var truckDto = new TruckInfoDto()
                {
                    CompanyName = companyInfo.CompanyName,
                    CompanyId = companyInfo.Id,
                    FrontTruckNo = truckNo
                };

                waitForValidateTruckList.Add(truckDto);
            }

            // 注册新增车辆信息
            _truckInfoService.QueryAfterValidateAndRegist(waitForValidateTruckList);
        }

    }
}