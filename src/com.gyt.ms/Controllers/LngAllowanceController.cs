using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Castle.Core.Internal;
using Zer.AppServices;
using Zer.Entities;
using Zer.Framework.Attributes;
using Zer.Framework.Cache;
using Zer.Framework.Exception;
using Zer.Framework.Export;
using Zer.Framework.Export.Attributes;
using Zer.Framework.Import;
using Zer.Framework.Mvc.Logs.Attributes;
using Zer.GytDto;
using Zer.GytDto.SearchFilters;
using Zer.Framework.Extensions;

namespace com.gyt.ms.Controllers
{
    [RoutePrefix("lng")]
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

        [UserActionLog("LNG补贴信息首页",ActionType.查询)]
        [Route("{filter?}")]
        public ActionResult Index(LngAllowanceSearchDto filter = null)
        {
            ViewBag.Filter = filter;

            var result = _lngAllowanceService.GetList(filter);
            return View(result);
        }

        [HttpGet]
        [Route("add")]
        public ActionResult Add()
        {
            ViewBag.ProvinceList = CacheHelper.GetCache("Province").ToString().PartString(',');
            ViewBag.CharacterList = CacheHelper.GetCache("Character").ToString().PartString(',');
            return View();
        }

        [UserActionLog("LNG补贴信息导出", ActionType.查询)]
        [Route("export")]
        public FileResult Export(LngAllowanceSearchDto searchDto)
        {
            if (searchDto == null) return null;

            searchDto.PageSize = Int32.MaxValue;
            searchDto.PageIndex = 1;
            var exportList = _lngAllowanceService.GetList(searchDto);

            return exportList == null ? null : ExportCsv(exportList.GetBuffer(), string.Format("LNG补贴信息{0:yyyyMMddhhmmssfff}", DateTime.Now));
        }

        [HttpPost]
        [Route("desc")]
        [UserActionLog("添加备注信息", ActionType.编辑)]
        [ValidateAntiForgeryToken]
        public JsonResult AddDescription(string id, string desc)
        {
            _lngAllowanceService.AddDescription(id, desc);
            return Success();
        }

        //Todo: 建议优化检查检查重复业务逻辑
        [HttpPost]
        [Route("import")]
        [UserActionLog("LNG补贴信息批量导入", ActionType.新增)]
        [ValidateAntiForgeryToken]
        public ActionResult ImportFile(HttpPostedFileBase file)
        {
            if (file == null || file.InputStream == null) throw new Exception("文件上传失败，导入失败");

            SaveFile(file,"lngallowance");

            var excelImport = new ExcelImport<LngAllowanceInfoDto>(file.InputStream);
            var lngAllowanceInfoDtoList = excelImport.Read(out var failedMessageList);

            if (lngAllowanceInfoDtoList.IsNullOrEmpty()) throw new Exception("没有从文件中读取到任何数据，导入失败，请重试!");

            var sessionCode = AppendObjectToSession(lngAllowanceInfoDtoList);
            var failedMessageListCode = AppendObjectToSession(failedMessageList);

            return RedirectToAction("SaveLngAllowanceData", "LngAllowance",
                new { id = sessionCode, errorMessageCode = failedMessageListCode });
        }

        [ReplaceSpecialCharInParameter("-", "_")]
        [GetParameteFromSession("id")]
        [UnLog]
        [Route("save/{id}/{errorMessageCode}")]
        public ActionResult SaveLngAllowanceData(string id,string errorMessageCode)
        {
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

            var dic = mustImportLngAllowanceInfoDtoList.Select(x => new {x.TruckNo, x.CompanyId}).Distinct()
                                                       .ToDictionary(x => x.TruckNo, v => v.CompanyId);

            // 初始化检测并注册其中的新车辆信息
            InitTruckInfoDtoList(dic, companyInfoDtoList);

            // 保存LNG补贴信息，并得到保存成功的结果
            var importSuccessList = _lngAllowanceService.AddRange(mustImportLngAllowanceInfoDtoList);

            var importFailedList = mustImportLngAllowanceInfoDtoList.Where(x => importSuccessList.Contains(x))
                .ToList();

            // 展示导入结果
            ViewBag.SuccessCode = AppendObjectToSession(importSuccessList);
            ViewBag.FailedCode = AppendObjectToSession(importFailedList);
            ViewBag.ExistedCode = AppendObjectToSession(existsLngAllowanceInfoDtoList);

            ViewBag.SuccessList = importSuccessList;
            ViewBag.FailedList = importFailedList;
            ViewBag.ExistedList = existsLngAllowanceInfoDtoList;
            ViewBag.errorMessageCode = GetValueFromSession<List<string>>(errorMessageCode);
            return View("ImportResult");
        }

        [HttpPost]
        [Route("addpost")]
        [ReplaceSpecialCharInParameter("-", "_")]
        [UserActionLog("LNG补贴信息单条新增", ActionType.新增)]
        [ValidateAntiForgeryToken]
        public JsonResult AddPost(LngAllowanceInfoDto dto)
        {
            if (_lngAllowanceService.Exists(dto))
            {
                return Fail("该车辆相关的车牌号或者气罐已经存在记录，请通过搜索及审核来管理补贴状态。");
            }
            var companyInfoDto = _companyService.QueryAfterValidateAndRegist(dto.CompanyName);

            dto.CompanyId = companyInfoDto.Id;
            dto.CreateTime = DateTime.Now;
            dto.Status = LngStatus.已补贴;

            _lngAllowanceService.Add(dto);

            return Success();
        }

        [HttpPost]
        [Route("status")]
        [UserActionLog("LNG补贴信息补贴状态更改", ActionType.更改状态)]
        [ValidateAntiForgeryToken]
        public JsonResult ChangStatus(string infoId)
        {
            var infoDto = _lngAllowanceService.GetById(infoId);
            if (infoDto.Status == LngStatus.已补贴)
            {
                return Fail("这条记录已是补贴状态，请核实！");
            }

            infoDto = _lngAllowanceService.ChangStatus(infoId);
            return infoDto.Status != LngStatus.已补贴 ? Fail("失败，请联系系统管理人员！") : Success("修改补贴状态成功！");
        }

        [HttpGet]
        [Route("edit/{infoId}")]
        public ActionResult Edit(string infoId)
        {
            ViewBag.ProvinceList = CacheHelper.GetCache("Province").ToString().PartString(',');
            ViewBag.CharacterList = CacheHelper.GetCache("Character").ToString().PartString(',');
            var infoDto = _lngAllowanceService.GetById(infoId);

            return View(infoDto);
        }

        [AdminRole]
        [HttpPost]
        [Route("sedit")]
        [UserActionLog("编辑LNG补贴信息",ActionType.编辑)]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEdit(LngAllowanceInfoDto lngAllowanceInfoDto)
        {
            if (lngAllowanceInfoDto.Id.IsNullOrEmpty())
            {
                throw new CustomException("LNG补贴编号不能为空");
            }

            var sourceDto = _lngAllowanceService.GetById(lngAllowanceInfoDto.Id);

            var companyInfo = _companyService.QueryAfterValidateAndRegist(lngAllowanceInfoDto.CompanyName);
            var trcukInfo = _truckInfoService.QueryAfterValidateAndRegist(new TruckInfoDto()
            {
                CompanyId = companyInfo.Id,
                CompanyName = companyInfo.CompanyName,
                FrontTruckNo = lngAllowanceInfoDto.TruckNo,
            });

            sourceDto.CompanyName = companyInfo.CompanyName;
            sourceDto.CompanyId = companyInfo.Id;
            sourceDto.LotId = lngAllowanceInfoDto.LotId;
            sourceDto.TruckNo = lngAllowanceInfoDto.TruckNo;
            sourceDto.EngineId = lngAllowanceInfoDto.EngineId;
            sourceDto.CylinderDefaultId = lngAllowanceInfoDto.CylinderDefaultId;
            sourceDto.CylinderSeconedId = lngAllowanceInfoDto.CylinderSeconedId;
            sourceDto.CreateTime = lngAllowanceInfoDto.CreateTime;
            sourceDto.Status = lngAllowanceInfoDto.Status;

            _lngAllowanceService.Edit(lngAllowanceInfoDto);
            return RedirectToAction("index", "LngAllowance");
        }
        
        [HttpPost]
        [Route("force")]
        [UserActionLog("强制导入数据", ActionType.编辑)]
        [ValidateAntiForgeryToken]
        public JsonResult ForceImport(List<LngAllowanceInfoDto> list)
        {
            _lngAllowanceService.ForceImport(list);
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
                    companyInfoDtoList.FirstOrDefault(x => x.CompanyName.Trim() == lngAllowanceInfoDto.CompanyName.Trim());

                lngAllowanceInfoDto.CompanyId = currentComapnyInfoDto?.Id ?? 0;
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