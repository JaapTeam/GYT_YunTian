using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Castle.Core.Internal;
using Zer.AppServices;
using Zer.Entities;
using Zer.Framework.Export;
using Zer.GytDto;

namespace com.gyt.ms.Controllers
{
    public class GYTInfoController : BaseController
    {
        private readonly IGYTInfoService _gytInfoService;
        private readonly ICompanyService _companyService;
        private readonly ITruckInfoService _truckInfoService;

        public GYTInfoController(IGYTInfoService gytInfoService, ICompanyService companyService, ITruckInfoService truckInfoService)
        {
            _gytInfoService = gytInfoService;
            _companyService = companyService;
            _truckInfoService = truckInfoService;
        }

        // GET: GYTInfo
        public ActionResult Index(int activeId=3)
        {
            ViewBag.ActiveId = activeId;

            ViewBag.CompanyList = _companyService.GetAll();
            ViewBag.TruckList = _truckInfoService.GetAll();
            ViewBag.Result =  _gytInfoService.GetAll().Where(x => x.Status == BusinessState.已通过).ToList();
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Improt(HttpPostedFileBase file)
        {
            if (file != null)
            {

                List<GYTInfoDto> gtyGytInfoDtoList;

                using (StreamReader reader = new StreamReader(file.InputStream, Encoding.Default))
                {
                    gtyGytInfoDtoList = Zer.Framework.Import.Import.Read<GYTInfoDto>(reader, 1);

                    if (gtyGytInfoDtoList != null)
                    {
                        //overloadRecrodDtoList.ForEach(ValidataInputString);

                        // 检测数据库中已经存在的重复数据
                        var existsgtyGytInfoDtoList = gtyGytInfoDtoList
                                                       .Where(x => _gytInfoService.Exists(x.BidTruckNo))
                                                       .ToList();

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
                                CompanyId = gtyGytInfoDto.OriginalCompanyId
                            });
                        }

                        // 初始化检测并注册其中的新车辆信息
                        InitTruckInfoDtoList(truckInfoDtoList);

                        mustImportgtyGytInfoDtoList.ForEach(x=>x.Status=BusinessState.已通过);

                        // 保存信息，并得到保存成功的结果
                        var importSuccessList = _gytInfoService.AddRange(mustImportgtyGytInfoDtoList);

                        var importFailedList = mustImportgtyGytInfoDtoList.Where(x => importSuccessList.Contains(x))
                            .ToList();

                        // 展示导入结果
                        ViewBag.ActiveId = 6;

                        ViewBag.SuccessCode = AppendObjectToSession(importSuccessList);
                        ViewBag.FailedCode = AppendObjectToSession(importFailedList);
                        ViewBag.ExistedCode = AppendObjectToSession(mustImportgtyGytInfoDtoList);

                        ViewBag.SuccessList = importSuccessList;
                        ViewBag.FailedList = importFailedList;
                        ViewBag.ExistedList = existsgtyGytInfoDtoList;
                        return View("ImportResult");

                    }
                }
            }
            return View();
        }

        public FileResult ExportResult(string exportCode = "")
        {
            List<GYTInfoDto> exportList = new List<GYTInfoDto>();

            if (exportCode.IsNullOrEmpty())
            {
                return null;
            }

            if (exportCode.ToLower() == "all")
            {
                exportList = _gytInfoService.GetAll();
            }
            else
            {
                exportList = GetValueFromSession<List<GYTInfoDto>>(exportCode);
            }

            return exportList == null ? null : ExportCsv(exportList.GetBuffer(), string.Format("超载超限记录{0:yyyyMMddhhmmssfff}", DateTime.Now));
        }

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
                var currentOriginalComapnyInfoDto =
                    companyInfoDtoList.Single(x => x.CompanyName == gtGytInfoDto.OriginalCompanyName);

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
    }
}