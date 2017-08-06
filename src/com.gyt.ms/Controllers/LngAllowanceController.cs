using System.Web.Mvc;
using Zer.AppServices;
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

        [HttpPost]
        public JsonResult AddPost(LngAllowanceInfoDto dto)
        {
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