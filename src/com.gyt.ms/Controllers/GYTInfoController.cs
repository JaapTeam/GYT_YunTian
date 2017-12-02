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
using Zer.Framework.Cache;
using Zer.Framework.Exception;
using Zer.Framework.Export;
using Zer.Framework.Import;
using Zer.Framework.Mvc.Logs.Attributes;
using Zer.GytDto;
using Zer.GytDto.SearchFilters;
using Zer.Framework.Extensions;

namespace com.gyt.ms.Controllers
{
    [RoutePrefix("gyt")]
    public class GYTInfoController : BaseController
    {
        private readonly IGYTInfoService _gytInfoService;
        private readonly ICompanyService _companyService;
        private readonly ITruckInfoService _truckInfoService;
        private readonly IPeccancyRecrodService _peccancyRecrodService;

        public GYTInfoController(
            IGYTInfoService gytInfoService,
            ICompanyService companyService,
            ITruckInfoService truckInfoService,
            IPeccancyRecrodService peccancyRecrodService)
        {
            _gytInfoService = gytInfoService;
            _companyService = companyService;
            _truckInfoService = truckInfoService;
            _peccancyRecrodService = peccancyRecrodService;
        }

        // GET: GYTInfo
        [UserActionLog("港运通信息数据库", ActionType.查询)]
        [Route("")]
        public ActionResult Index(GYTInfoSearchDto searchDto)
        {
            ViewBag.CompanyList = _companyService.GetAll();
            ViewBag.TruckList = _truckInfoService.GetAll();
            ViewBag.SearchDto = searchDto;
            ViewBag.Result = _gytInfoService.GetList(searchDto);
            return View();
        }

        [HttpPost]
        [UserActionLog("港运通信息数据导入", ActionType.新增)]
        [Route("import")]
        [ValidateAntiForgeryToken]
        public ActionResult Improt(HttpPostedFileBase file)
        {
            if (file == null || file.InputStream == null)
                throw new Exception("文件上传失败，导入失败");

            SaveFile(file,"gyt");

            var excelImport = new ExcelImport<GYTInfoDto>(file.InputStream);

            var gtyGytInfoDtoList = excelImport.Read(out var failedErrorMessageList);

            if (gtyGytInfoDtoList.IsNullOrEmpty()) throw new Exception("没有从文件中读取到任何数据，导入失败，请重试!");

            // 检测数据库中已经存在的重复数据

            var bidTruckNoList = gtyGytInfoDtoList.Select(x => x.BidTruckNo).ToList();

            var existsgtyGytInfoDtoList = _gytInfoService.GetListByBidTruckNoList(bidTruckNoList);

            // 筛选出需要导入的数据
            var mustImportgtyGytInfoDtoList =
                gtyGytInfoDtoList
                    .Where(x => !existsgtyGytInfoDtoList
                        .Select(info => info.BidTruckNo)
                        .Contains(x.BidTruckNo)).ToList();

            // 初始化检测并注册其中的新公司信息
            InitCompanyInfoDtoList(mustImportgtyGytInfoDtoList);

            //var dic = mustImportoverloadRecrodDtoList.ToDictionary(x => x.PeccancyId, v => v.CompanyId);
            var truckInfoDtoList = new List<TruckInfoDto>();
            foreach (var gtyGytInfoDto in mustImportgtyGytInfoDtoList)
            {
                truckInfoDtoList.Add(new TruckInfoDto
                {
                    FrontTruckNo = gtyGytInfoDto.BidTruckNo,
                    CompanyId = gtyGytInfoDto.BidCompanyId
                });

                truckInfoDtoList.Add(new TruckInfoDto
                {
                    FrontTruckNo = gtyGytInfoDto.OriginalTruckNo,
                    CompanyId = gtyGytInfoDto.OriginalCompanyId ?? 0
                });
            }

            // 初始化检测并注册其中的新车辆信息
            InitTruckInfoDtoList(truckInfoDtoList);

            //mustImportgtyGytInfoDtoList.ForEach(x => x.Status = BusinessState.已注销);

            // 保存信息，并得到保存成功的结果
            var importSuccessList = _gytInfoService.AddRange(mustImportgtyGytInfoDtoList);

            var importFailedList = mustImportgtyGytInfoDtoList.Where(x => importSuccessList.Contains(x))
                .ToList();

            // 展示导入结果


            ViewBag.SuccessCode = AppendObjectToSession(importSuccessList);
            ViewBag.FailedCode = AppendObjectToSession(importFailedList);
            ViewBag.ExistedCode = AppendObjectToSession(mustImportgtyGytInfoDtoList);

            ViewBag.SuccessList = importSuccessList;
            ViewBag.FailedList = importFailedList;
            ViewBag.ExistedList = existsgtyGytInfoDtoList;
            ViewBag.FormatErrorList = failedErrorMessageList;
            return View("ImportResult");
        }

        [UserActionLog("港运通信息数据导出", ActionType.查询)]
        [Route("export")]
        [ValidateAntiForgeryToken]
        public FileResult ExportResult(GYTInfoSearchDto searchDto)
        {
            searchDto.PageSize = Int32.MaxValue;
            searchDto.PageIndex = 1;
            var exportList = _gytInfoService.GetList(searchDto);

            return exportList == null ? null : ExportCsv(exportList.GetBuffer(), string.Format("港运通信息数据{0:yyyyMMddhhmmssfff}", DateTime.Now));
        }

        [AdminRole]
        [UserActionLog("港运通审核", ActionType.更改状态)]
        [Route("verify/{infoId}")]
        [ValidateAntiForgeryToken]
        public JsonResult Verify(string infoId)
        {
            var gtyInfo = _gytInfoService.GetById(infoId);

            if (gtyInfo.Status == BusinessState.已办理)
            {
                return Fail("这条记录已经审核！");
            }

            gtyInfo = _gytInfoService.Verify(infoId);

            if (gtyInfo.Status != BusinessState.已办理)
            {
                return Fail("审核失败！");
            }

            return Success("审核成功！");
        }

        [Route("edit/{infoId}")]
        public ActionResult Edit(string infoId)
        {
            ViewBag.ProvinceList = CacheHelper.GetCache("Province").ToString().PartString(',');
            ViewBag.CharacterList = CacheHelper.GetCache("Character").ToString().PartString(',');
            var infoDto = _gytInfoService.GetById(infoId);
            return View(infoDto);
        }

        [AdminRole]
        [UserActionLog("港运通信息编辑", ActionType.编辑)]
        [Route("se")]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEdit(GYTInfoDto dto)
        {
            if (dto.Id.IsNullOrEmpty())
            {
                throw new CustomException("港运通编号不能为空");
            }

            var companyInfoList = _companyService.QueryAfterValidateAndRegist(new List<CompanyInfoDto>()
            {
                new CompanyInfoDto(){CompanyName = dto.BidCompanyName},
                new CompanyInfoDto(){CompanyName = dto.OriginalCompanyName.IsNullOrEmpty()?"":dto.OriginalCompanyName}
            });


            var trcuInfoDtoList = new List<TruckInfoDto>();

            var bidCompanyInfo = companyInfoList.FirstOrDefault(x => x.CompanyName == dto.BidCompanyName);
            trcuInfoDtoList.Add(new TruckInfoDto()
            {
                CompanyName = bidCompanyInfo == null ? "" : bidCompanyInfo.CompanyName,
                CompanyId = bidCompanyInfo == null ? 0 : bidCompanyInfo.Id,
                FrontTruckNo = dto.BidTruckNo
            });

            var originalCompanyInfo = companyInfoList.FirstOrDefault(x => x.CompanyName == dto.OriginalCompanyName);
            trcuInfoDtoList.Add(new TruckInfoDto()
            {
                CompanyName = originalCompanyInfo == null ? "" : originalCompanyInfo.CompanyName,
                CompanyId = originalCompanyInfo == null ? 0 : originalCompanyInfo.Id,
                FrontTruckNo = dto.OriginalTruckNo
            });

            _truckInfoService.QueryAfterValidateAndRegist(trcuInfoDtoList);

            dto.BidDisplayName = CurrentUser.DisplayName;
            dto.BidName = CurrentUser.UserName;
            dto.BidDate = DateTime.Now;

            _gytInfoService.Edit(dto);
            return RedirectToAction("index", "GYTInfo");
        }

        //private bool saveChanges(GYTInfoDto inputDto, GYTInfoDto originalDto)
        //{
        //    originalDto.BidCompanyName = inputDto.BidCompanyName;
        //    originalDto.BidTruckNo = inputDto.BidTruckNo;

        //    if (originalDto.BusinessType != BusinessType.天然气车辆)
        //    {
        //        originalDto.OriginalCompanyName = inputDto.OriginalCompanyName;
        //        originalDto.OriginalTruckNo = inputDto.OriginalTruckNo;
        //    }

        //    originalDto.BidDisplayName = CurrentUser.DisplayName;
        //    originalDto.BidName = CurrentUser.UserName;

        //    originalDto.BidCompanyId = _companyService.QueryAfterValidateAndRegist(originalDto.BidCompanyName).Id;
        //    originalDto.OriginalCompanyId = _companyService.QueryAfterValidateAndRegist(originalDto.OriginalCompanyName)
        //        .Id;

        //    _truckInfoService.QueryAfterValidateAndRegist(new List<TruckInfoDto>
        //    {
        //        new TruckInfoDto()
        //        {
        //            FrontTruckNo = originalDto.BidTruckNo,
        //            CompanyId = originalDto.BidCompanyId,
        //            CompanyName = originalDto.BidCompanyName
        //        },
        //        new TruckInfoDto()
        //        {
        //            FrontTruckNo = originalDto.OriginalTruckNo,
        //            CompanyId = originalDto.OriginalCompanyId ?? 0,
        //            CompanyName = originalDto.OriginalCompanyName
        //        }
        //    });

        //    var result = _gytInfoService.Edit(originalDto);

        //    return result != null;
        //}

        private List<CompanyInfoDto> InitCompanyInfoDtoList(List<GYTInfoDto> gtGytInfoDtos)
        {
            var improtCompanyInfoDtoList = new List<CompanyInfoDto>();
            foreach (var gtGytInfoDto in gtGytInfoDtos)
            {
                improtCompanyInfoDtoList.Add(
                    new CompanyInfoDto
                    {
                        CompanyName = gtGytInfoDto.BidCompanyName
                    }
                );

                improtCompanyInfoDtoList.Add(
                    new CompanyInfoDto
                    {
                        CompanyName = gtGytInfoDto.OriginalCompanyName
                    }
                );
            }

            // 注册新增公司信息
            var companyInfoDtoList = _companyService.QueryAfterValidateAndRegist(improtCompanyInfoDtoList);

            foreach (var gtGytInfoDto in gtGytInfoDtos)
            {
                var currentOriginalComapnyInfoDto = new CompanyInfoDto();
                if (gtGytInfoDto.BusinessType != BusinessType.天然气车辆)
                {
                     currentOriginalComapnyInfoDto =
                        companyInfoDtoList.Single(x => x.CompanyName == gtGytInfoDto.OriginalCompanyName);
                }
                
                var currentBidComapnyInfoDto =
                    companyInfoDtoList.Single(x => x.CompanyName == gtGytInfoDto.BidCompanyName);

                gtGytInfoDto.OriginalCompanyId = currentOriginalComapnyInfoDto.Id;
                gtGytInfoDto.BidCompanyId = currentBidComapnyInfoDto.Id;
            }

            return companyInfoDtoList;
        }

        private void InitTruckInfoDtoList(List<TruckInfoDto> truckInfoDtos)
        {
            // 注册新增车辆信息
            _truckInfoService.QueryAfterValidateAndRegist(truckInfoDtos);
        }

        private List<string> CommonValidate(GYTInfoDto dto)
        {
            var result = new List<string>();

            if (dto.BidCompanyName.IsNullOrEmpty())
            {
                result.Add("申办企业名称不能为空");
                return result;
            }

            if (dto.BidTruckNo.Substring(2).IsNullOrEmpty())
            {
                result.Add("申办车牌号不能为空");
                return result;
            }

            // 申办企业不能有违法记录
            if (_peccancyRecrodService.ExistsCompanyName(dto.BidCompanyName))
            {
                result.Add("申办企业存在超载超限记录,不符合修改条件");
            }

            // 新申请车牌不能存在已办理记录
            var recordDto = _gytInfoService.GetByBidTruckNo(dto.BidTruckNo);
            if (recordDto == null)
            {
                result.Add(string.Format("申办车牌号[{0}]不存在办理港运通办理记录,不符合修改条件", dto.BidTruckNo));
            }

            return result;
        }

        private List<string> ValidateWithGasBusiness(GYTInfoDto dto)
        {
            // 天然气业务
            return new List<string>();
        }

        private List<string> ValidateWithReplaceBusiness(GYTInfoDto dto)
        {
            // 以旧换新业务
            var result = new List<string>();

            var gytInfoDto = _gytInfoService.GetByBidTruckNo(dto.OriginalTruckNo);

            // 旧车必须有办理记录
            if (gytInfoDto == null)
            {
                result.Add(string.Format("原车牌 {0} 不存在港运通办理记录，或已注销，不符合修改条件", dto.OriginalTruckNo));
            }

            if (gytInfoDto != null && gytInfoDto.Status == BusinessState.已注销)
            {
                result.Add(string.Format(
                    "原车牌 {0} 与 港运通编号 {1} 的绑定关系已经被注销，以旧换新指标已经使用，不符合修改条件",
                    dto.OriginalTruckNo,
                    gytInfoDto.Id));
            }

            return result;
        }

        private List<string> ValidateWithTransferBusiness(GYTInfoDto dto)
        {
            // 车辆过户业务
            var result = new List<string>();

            var gytInfoDto = _gytInfoService.GetByBidTruckNo(dto.OriginalTruckNo);

            // 旧车必须有办理记录
            if (gytInfoDto == null)
            {
                result.Add(string.Format("原车牌 {0} 不存在港运通办理记录，不符合修改条件", dto.OriginalTruckNo));
            }

            if (gytInfoDto != null && gytInfoDto.Status == BusinessState.已注销)
            {
                result.Add(string.Format(
                    "原车牌 {0} 与 港运通编号 {1} 的绑定关系已经被注销，车辆过户指标已经使用，不符合修改条件",
                    dto.OriginalTruckNo,
                    gytInfoDto.Id));
            }


            return result;
        }
    }
}