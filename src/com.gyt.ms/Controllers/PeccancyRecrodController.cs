using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Core.Internal;
using Zer.AppServices;
using Zer.Entities;
using Zer.Framework.Cache;
using Zer.Framework.Export;
using Zer.Framework.Import;
using Zer.Framework.Mvc.Logs.Attributes;
using Zer.GytDto;
using Zer.GytDto.SearchFilters;
using Zer.Framework.Extensions;

namespace com.gyt.ms.Controllers
{
    //ToDo:还有Add功能没做
    public class PeccancyRecrodController : BaseController
    {
        private readonly IPeccancyRecrodService _peccancyRecrodService;
        private readonly ITruckInfoService _truckInfoService;
        private readonly ICompanyService _companyService;

        public PeccancyRecrodController(IPeccancyRecrodService overloadRecrodService, ITruckInfoService truckInfoService, ICompanyService companyService)
        {
            _peccancyRecrodService = overloadRecrodService;
            _truckInfoService = truckInfoService;
            _companyService = companyService;
        }

        [UserActionLog("超载超限记录查询", ActionType.查询)]
        public ActionResult Index(PeccancySearchDto searchDto)
        {
            ViewBag.SearchDto = searchDto;
            ViewBag.Result = _peccancyRecrodService.GetList(searchDto).ToList();
            return View();
        }

        [UserActionLog("公司违章记录统计", ActionType.查询)]
        public ActionResult Company(PeccancyWithCompanySearchDto dto)
        {
            ViewBag.Filter = dto;
            ViewBag.CompanyList = _peccancyRecrodService.GetPeccancyGroupByCompany(dto);

            return View();
        }

        [UserActionLog("公司违章记录统计信息导出", ActionType.查询)]
        public FileResult ExportPecancyWithCompany(PeccancyWithCompanySearchDto dto)
        {
            if (dto == null) return null;

            dto.PageSize = Int32.MaxValue;
            dto.PageIndex = 1;
            var exportList = _peccancyRecrodService.GetPeccancyGroupByCompany(dto);

            return exportList == null ? null : ExportCsv(exportList.GetBuffer(), string.Format("公司违章记录统计信息-{0:yyyyMMddhhmmssfff}", DateTime.Now));
        }

        [UserActionLog("超载超限记录整改状态变更", ActionType.更改状态)]
        public JsonResult Change(string id="")
        {
            if (id.IsNullOrEmpty())
            {
                return Fail("请选择需要整改的记录！");
            }

            var result = _peccancyRecrodService.ChangeStatusById(id);

            if (!result)
            {
                return Fail("整改失败，请联系系统管理人员！");
            }

            return Success("整改成功！");
        }

        //Todo: 建议优化检查检查重复业务逻辑
        [System.Web.Mvc.HttpPost]
        [UserActionLog("超载超限记录批量导入", ActionType.新增)]
        public ActionResult ImportFile(HttpPostedFileBase file)
        {
            if (file == null || file.InputStream == null) throw new Exception("文件上传失败，导入失败");

            var excelImport = new ExcelImport<PeccancyRecrodDto>(file.InputStream);
            var overloadRecrodDtoList = excelImport.Read();

            if (overloadRecrodDtoList.IsNullOrEmpty()) throw new Exception("没有从文件中读取到任何数据，导入失败，请重试!");

            // 检测数据库中已经存在的重复数据
            var existsoverloadRecrodDtoList = overloadRecrodDtoList
                .Where(x => _peccancyRecrodService.Exists(x))
                .ToList();

            // 筛选出需要导入的数据
            var mustImportoverloadRecrodDtoList =
                overloadRecrodDtoList
                    .Where(x => !existsoverloadRecrodDtoList
                        .Select(overLoad => overLoad.Id)
                        .Contains(x.Id)).ToList();

            // 初始化检测并注册其中的新公司信息
            InitCompanyInfoDtoList(mustImportoverloadRecrodDtoList);

            //var dic = mustImportoverloadRecrodDtoList.ToDictionary(x => x.PeccancyId, v => v.CompanyId);
            var truckInfoDtoList = new List<TruckInfoDto>();
            foreach (var overLoadDto in mustImportoverloadRecrodDtoList)
            {
                truckInfoDtoList.Add(new TruckInfoDto
                {
                    FrontTruckNo = overLoadDto.FrontTruckNo,
                    BehindTruckNo = overLoadDto.BehindTruckNo,
                    CompanyId = overLoadDto.CompanyId
                });
            }

            // 初始化检测并注册其中的新车辆信息
            InitTruckInfoDtoList(truckInfoDtoList);

            // 保存信息，并得到保存成功的结果
            var importSuccessList = _peccancyRecrodService.AddRange(mustImportoverloadRecrodDtoList);

            var importFailedList = mustImportoverloadRecrodDtoList.Where(x => importSuccessList.Contains(x))
                .ToList();

            // 展示导入结果
            ViewBag.SuccessCode = AppendObjectToSession(importSuccessList);
            ViewBag.FailedCode = AppendObjectToSession(importFailedList);
            ViewBag.ExistedCode = AppendObjectToSession(existsoverloadRecrodDtoList);

            ViewBag.SuccessList = importSuccessList;
            ViewBag.FailedList = importFailedList;
            ViewBag.ExistedList = existsoverloadRecrodDtoList;
            return View("ImportResult");
        }

        [UserActionLog("超载超限记录导出", ActionType.查询)]
        public FileResult ExportResult(PeccancySearchDto searchDto)
        {
            if (searchDto == null) return null;

            searchDto.PageSize = Int32.MaxValue;
            searchDto.PageIndex = 1;
            var exportList = _peccancyRecrodService.GetList(searchDto);

            return exportList == null ? null : ExportCsv(exportList.GetBuffer(), string.Format("超载超限信息数据库{0:yyyyMMddhhmmssfff}", DateTime.Now));
        }

        public ActionResult Edit(string peccancyId)
        {
            ViewBag.ProvinceList = CacheHelper.GetCache("Province").ToString().PartString(',');
            ViewBag.CharacterList = CacheHelper.GetCache("Character").ToString().PartString(',');
            var infoDto = _peccancyRecrodService.GetByPeccancyId(peccancyId);

            return View(infoDto);
        }

        [UserActionLog("编辑超载超限记录",ActionType.编辑)]
        public ActionResult SaveEdit(PeccancyRecrodDto infoDto)
        {
            if (infoDto.Id.IsNullOrEmpty())
            {
                return Fail("处罚决定书编号不能为空.");
            }

            var sourceDto = _peccancyRecrodService.GetByPeccancyId(infoDto.Id);
            sourceDto.BehindTruckNo = infoDto.BehindTruckNo;
            sourceDto.TraderRange = infoDto.TraderRange;
            sourceDto.DriverName = infoDto.DriverName;
            sourceDto.PeccancyDate = infoDto.PeccancyDate;
            sourceDto.PeccancyMatter = infoDto.PeccancyMatter;
            sourceDto.Source = infoDto.Source;
            sourceDto.Status = infoDto.Status;

            var companyInfo = _companyService.QueryAfterValidateAndRegist(infoDto.CompanyName);
            sourceDto.CompanyId = companyInfo.Id;

            var trcukInfo = _truckInfoService.QueryAfterValidateAndRegist(new TruckInfoDto()
            {
                CompanyId = companyInfo.Id,
                CompanyName = companyInfo.CompanyName,
                FrontTruckNo = infoDto.FrontTruckNo,
                BehindTruckNo = infoDto.BehindTruckNo,
                DriverId =  infoDto.DriverId,
                DriverName = infoDto.DriverName
            });

            _peccancyRecrodService.Edit(sourceDto);
            return RedirectToAction("Index", "PeccancyRecrod");
        }

        private List<CompanyInfoDto> InitCompanyInfoDtoList(List<PeccancyRecrodDto> overloadRecrodDtos)
        {
            var improtCompanyInfoDtoList = new List<CompanyInfoDto>();
            foreach (var overloadRecrodDto in overloadRecrodDtos)
            {
                improtCompanyInfoDtoList.Add(
                    new CompanyInfoDto
                    {
                        CompanyName = overloadRecrodDto.CompanyName,
                        TraderRange = overloadRecrodDto.TraderRange
                    }
                );
            }

            // 注册新增公司信息
            var companyInfoDtoList = _companyService.QueryAfterValidateAndRegist(improtCompanyInfoDtoList);

            foreach (var overloadRecrodDto in overloadRecrodDtos)
            {
                var currentComapnyInfoDto =
                    companyInfoDtoList.Single(x => x.CompanyName == overloadRecrodDto.CompanyName);

                overloadRecrodDto.CompanyId = currentComapnyInfoDto.Id;
            }
            return companyInfoDtoList;
        }

        private void InitTruckInfoDtoList(List<TruckInfoDto> truckInfoDtos)
        {
            // 注册新增车辆信息
            _truckInfoService.QueryAfterValidateAndRegist(truckInfoDtos);
        }
    }
}