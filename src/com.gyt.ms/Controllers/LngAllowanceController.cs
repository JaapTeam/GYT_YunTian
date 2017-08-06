using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
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

        public FileResult Export(int activeId = 9, int[] idList = null)
        {
            ViewBag.ActiveId = activeId;
            var list = _lngAllowanceService.GetList(idList);

            return ExportCsv(list.GetBuffer(), "LNG补贴信息");
        }

        public ViewResult Import(int activeId=9)
        {
            ViewBag.ActiveId = activeId;
            return View();
        }

        [HttpPost]
        public ActionResult ImportFile(HttpPostedFileBase file)
        {
            if (file != null)
            {
                List<LngAllowanceInfoDto> lngAllowanceInfoDtoList;
                using (StreamReader reader = new StreamReader(file.InputStream,Encoding.Default))
                {
                    lngAllowanceInfoDtoList = Zer.Framework.Import.Import.Read<LngAllowanceInfoDto>(reader, 1);
                }

                if (lngAllowanceInfoDtoList != null)
                {
                    // 替换气罐号中的 '-',
                    lngAllowanceInfoDtoList = lngAllowanceInfoDtoList.Select(ReplaceUnsafeChar).ToList();

                    // 然后检查导入数据是否包含非法字符
                    lngAllowanceInfoDtoList.ForEach(ValidataInputString);

                    // TODO: 未导入数据需要反馈显示给客户端

                    // 重复数据不导入
                    lngAllowanceInfoDtoList = lngAllowanceInfoDtoList
                                                .Where(x => !_lngAllowanceService.Exists(x))
                                                .ToList();

                    var companyNameList = lngAllowanceInfoDtoList.Select(x => x.CompanyName).ToList();

                    // 注册新增公司信息
                    var companyInfoDtoList = _companyService.QueryAfterValidateAndRegist(companyNameList);

                    foreach (var lngAllowanceInfoDto in lngAllowanceInfoDtoList)
                    {
                        var currentComapnyInfoDto =
                            companyInfoDtoList.Single(x => x.CompanyName == lngAllowanceInfoDto.CompanyName);

                        lngAllowanceInfoDto.CompanyId = currentComapnyInfoDto.Id;
                    }

                    var dic = lngAllowanceInfoDtoList.ToDictionary(x => x.TruckNo, v => v.CompanyId);

                    var waitForRegistTruckList = new List<TruckInfoDto>();

                    foreach (var truckNo in dic.Keys)
                    {
                        var companyInfo = companyInfoDtoList.Single(x => x.Id == dic[truckNo]);
                        var truckDto = new TruckInfoDto()
                        {
                            CompanyName = companyInfo.CompanyName,
                            CompanyId = companyInfo.Id,
                            FrontTruckNo = truckNo
                        };
                    }

                    // 注册新增车辆信息
                    _truckInfoService.AddRange(waitForRegistTruckList.ToList());

                    // 保存LNG补贴信息
                    _lngAllowanceService.AddRange(lngAllowanceInfoDtoList);

                    return RedirectToAction("Index", "LngAllowance", new { activeId = 9 });
                }
                   
            }

            return RedirectToAction("Index", "Error", "导入失败");
        }

        [HttpPost]
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
                    CompanyId =  companyInfoDto.Id,
                    CompanyName = companyInfoDto.CompanyName,
                    FrontTruckNo = dto.TruckNo
                });
            }

            _lngAllowanceService.Add(dto);

            return Success();
        }
    }
}