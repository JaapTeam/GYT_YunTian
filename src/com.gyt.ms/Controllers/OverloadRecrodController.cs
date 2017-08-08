using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Castle.Core.Internal;
using Zer.AppServices;
using Zer.Framework.Export;
using Zer.GytDto;
using Zer.Services;

namespace com.gyt.ms.Controllers
{
    //ToDo:还有Add功能没做
    public class OverloadRecrodController : BaseController
    {
        private readonly IOverloadRecrodService _overloadRecrodService;
        private readonly ITruckInfoService _truckInfoService;
        private readonly ICompanyService _companyService;

        public OverloadRecrodController(IOverloadRecrodService overloadRecrodService, ITruckInfoService truckInfoService, ICompanyService companyService)
        {
            _overloadRecrodService = overloadRecrodService;
            _truckInfoService = truckInfoService;
            _companyService = companyService;
        }

        public ActionResult Index(int activeId=0)
        {
            ViewBag.ActiveId = activeId;
            ViewBag.TruckList = _truckInfoService.GetAll().ToList();
            ViewBag.CompanyList = _companyService.GetAll().ToList();
            ViewBag.Result = _overloadRecrodService.GetAll();
            return View();
        }

        //ToDo:单元测试
        public JsonResult Chang(int id=0)
        {
            if (id == 0)
            {
                return Fail("请选择需要整改的记录！");
            }

            var result = _overloadRecrodService.ChangedById(id);

            if (!result)
            {
                return Fail();
            }

            return Success();
        }

        //ToDo:单元测试
        public FileResult Export(int[] ids)
        {
            if (ids.Length<=0)
            {
                RedirectToAction("index", "Error", "请选择需要导出的记录！");
            }

            var result = _overloadRecrodService.GetListByIds(ids);

            if (result.Count<=0)
            {
                RedirectToAction("index", "Error", "未查询到相关数据！");
            }

            return ExportCsv(result.GetBuffer(), "违法记录");
        }

        public ActionResult ImportFile(HttpPostedFileBase file)
        {
            if (file != null)
            {
                List<OverloadRecrodDto> overloadRecrodDtoList;

                using (StreamReader reader = new StreamReader(file.InputStream, Encoding.Default))
                {
                    overloadRecrodDtoList = Zer.Framework.Import.Import.Read<OverloadRecrodDto>(reader, 1);

                    if (overloadRecrodDtoList != null)
                    {
                        //overloadRecrodDtoList.ForEach(ValidataInputString);

                        // 检测数据库中已经存在的重复数据
                        var existsoverloadRecrodDtoList = overloadRecrodDtoList
                                                       .Where(x => _overloadRecrodService.Exists(x))
                                                       .ToList();

                        // 筛选出需要导入的数据
                        var mustImportoverloadRecrodDtoList =
                            overloadRecrodDtoList
                                .Where(x => !existsoverloadRecrodDtoList
                                    .Select(overLoad => overLoad.PeccancyId)
                                    .Contains(x.PeccancyId)).ToList();

                        // 初始化检测并注册其中的新公司信息
                        var companyInfoDtoList = InitCompanyInfoDtoList(mustImportoverloadRecrodDtoList);

                        var dic = mustImportoverloadRecrodDtoList.ToDictionary(x => x.PeccancyId, v => v.CompanyId);

                        // 初始化检测并注册其中的新车辆信息
                        InitTruckInfoDtoList(dic, companyInfoDtoList);

                        // 保存信息，并得到保存成功的结果
                        var importSuccessList = _overloadRecrodService.AddRange(mustImportoverloadRecrodDtoList);

                        var importFailedList = mustImportoverloadRecrodDtoList.Where(x => importSuccessList.Contains(x))
                            .ToList();

                        // 展示导入结果
                        ViewBag.ActiveId = 9;

                        ViewBag.SuccessCode = AppendObjectToSession(importSuccessList);
                        ViewBag.FailedCode = AppendObjectToSession(importFailedList);
                        ViewBag.ExistedCode = AppendObjectToSession(existsoverloadRecrodDtoList);

                        ViewBag.SuccessList = importSuccessList;
                        ViewBag.FailedList = importFailedList;
                        ViewBag.ExistedList = existsoverloadRecrodDtoList;
                        return View("ImportResult");

                    }
                }

            }
            return RedirectToAction("Index", "Error", "导入失败");
        }

        public FileResult ExportResult(string exportCode = "")
        {
            List<OverloadRecrodDto> exportList = new List<OverloadRecrodDto>();

            if (exportCode.IsNullOrEmpty())
            {
                return null;
            }

            if (exportCode.ToLower() == "all")
            {
                exportList = _overloadRecrodService.GetAll();
            }
            else
            {
                exportList = GetValueFromSession<List<OverloadRecrodDto>>(exportCode);
            }

            return exportList == null ? null : ExportCsv(exportList.GetBuffer(), string.Format("超载超限记录{0:yyyyMMddhhmmssfff}", DateTime.Now));
        }

        private List<CompanyInfoDto> InitCompanyInfoDtoList(List<OverloadRecrodDto> overloadRecrodDtos)
        {
            var companyNameList = overloadRecrodDtos.Select(x => x.CompanyName).ToList();

            // 注册新增公司信息
            var companyInfoDtoList = _companyService.QueryAfterValidateAndRegist(companyNameList);

            foreach (var overloadRecrodDto in overloadRecrodDtos)
            {
                var currentComapnyInfoDto =
                    companyInfoDtoList.Single(x => x.CompanyName == overloadRecrodDto.CompanyName);

                overloadRecrodDto.CompanyId = currentComapnyInfoDto.Id;
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