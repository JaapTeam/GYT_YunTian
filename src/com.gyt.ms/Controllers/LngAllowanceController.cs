using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Castle.Components.DictionaryAdapter;
using Zer.AppServices;
using Zer.Entities;
using Zer.Framework.Export;
using Zer.GytDto;

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
            var dto = _lngAllowanceService.GetAll();
            return View(dto);
        }

        public ActionResult Add(int activeId = 9)
        {
            ViewBag.ActiveId = activeId;
            ViewBag.CompanyList = _companyService.GetAll();
            return View();
        }

        public FileResult Export([FromBody]List<LngAllowanceInfoDto> exportList = null)
        {
            // TODO: 导入参数传入问题有待解决
            if (exportList == null)
            {
                exportList = _lngAllowanceService.GetAll().ToList();
            }

            return ExportCsv(exportList.GetBuffer(), "LNG补贴信息");
        }

        public ViewResult Import(int activeId = 9)
        {
            ViewBag.ActiveId = activeId;
            return View();
        }

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
                    // 替换气罐号中的 '-',
                    lngAllowanceInfoDtoList = lngAllowanceInfoDtoList.Select(ReplaceUnsafeChar).ToList();

                    // 然后检查导入数据是否包含非法字符
                    lngAllowanceInfoDtoList.ForEach(ValidataInputString);

                    // 检测数据库中已经存在的重复数据
                    var existsLngAllowanceInfoDtoList = lngAllowanceInfoDtoList
                                                            .Where(x => _lngAllowanceService.Exists(x))
                                                            .ToList();

                    // 筛选出需要导入的数据
                    var mustImportLngAllowanceInfoDtoList =
                        lngAllowanceInfoDtoList.Where(x => !existsLngAllowanceInfoDtoList.Select(lng => lng.TruckNo).Contains(x.TruckNo)).ToList();

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
                    ViewBag.SuccessList = importSuccessList;
                    ViewBag.FailedList = importFailedList;
                    ViewBag.ExistedList = existsLngAllowanceInfoDtoList;
                    return View("ImportResult");
                }
            }

            return RedirectToAction("Index", "Error", "导入失败");
        }
       
        [System.Web.Mvc.HttpPost]
        public JsonResult AddPost(LngAllowanceInfoDto dto)
        {
            dto = ReplaceUnsafeChar(dto);
            ValidataInputString(dto);

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
            var companyNameList = lngAllowanceInfoDtoList.Select(x => x.CompanyName).ToList();

            // 注册新增公司信息
            var companyInfoDtoList = _companyService.QueryAfterValidateAndRegist(companyNameList);

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