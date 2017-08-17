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
using Zer.Framework.Export;
using Zer.Framework.Import;
using Zer.GytDto;
using Zer.GytDto.SearchFilters;
using Zer.Services;

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

        public ActionResult Index(PeccancySearchDto searchDto, int activeId = 9)
        {
            ViewBag.ActiveId = activeId;
            ViewBag.SearchDto = searchDto;
            ViewBag.Result = _peccancyRecrodService.GetList(searchDto).Where(x => x.Status == Status.未整改).ToList();
            return View();
        }

        //ToDo:单元测试
        public JsonResult Change(int id=0)
        {
            if (id == 0)
            {
                return Fail("请选择需要整改的记录！");
            }

            var result = _peccancyRecrodService.ChangeStatusById(id);

            if (!result)
            {
                return Fail();
            }

            return Success();
        }

        //Todo: 建议优化检查检查重复业务逻辑
        [System.Web.Mvc.HttpPost]
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
                        .Select(overLoad => overLoad.PeccancyId)
                        .Contains(x.PeccancyId)).ToList();

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
            ViewBag.ActiveId = 6;

            ViewBag.SuccessCode = AppendObjectToSession(importSuccessList);
            ViewBag.FailedCode = AppendObjectToSession(importFailedList);
            ViewBag.ExistedCode = AppendObjectToSession(existsoverloadRecrodDtoList);

            ViewBag.SuccessList = importSuccessList;
            ViewBag.FailedList = importFailedList;
            ViewBag.ExistedList = existsoverloadRecrodDtoList;
            return View("ImportResult");
        }

        public FileResult ExportResult(string exportCode = "")
        {
            List<PeccancyRecrodDto> exportList = new List<PeccancyRecrodDto>();

            if (exportCode.IsNullOrEmpty())
            {
                return null;
            }

            if (exportCode.ToLower() == "all")
            {
                exportList = _peccancyRecrodService.GetAll().Where(x=>x.Status==Status.未整改).ToList();
            }
            else
            {
                exportList = GetValueFromSession<List<PeccancyRecrodDto>>(exportCode);
            }

            return exportList == null ? null : ExportCsv(exportList.GetBuffer(), string.Format("超载超限未整改记录{0:yyyyMMddhhmmssfff}", DateTime.Now));
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Search(PeccancySearchDto searchDto, int activeId = 9)
        {
            ViewBag.ActiveId = activeId;
            var truckList = _truckInfoService.GetAll();
            var companyList = _companyService.GetAll();

            ViewBag.TruckList = truckList;
            ViewBag.CompanyList = companyList;
            ViewBag.SearchDto = searchDto;

            searchDto.Status = Status.未整改;
            ViewBag.Result = _peccancyRecrodService.GetList(searchDto);
            
            return View("Index");
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