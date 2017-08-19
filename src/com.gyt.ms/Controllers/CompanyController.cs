using System;
using System.Linq;
using System.Web.Mvc;
using Zer.AppServices;
using Zer.Entities;
using Zer.Framework.Entities;
using Zer.Framework.Extensions;
using Zer.GytDto;

namespace com.gyt.ms.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public ActionResult Index()
        {
           
            var dto = _companyService.GetById(1);
            return View(dto);
        }

        public JsonResult AddCompany(CompanyInfoDto companyInfoDto)
        {
            var companyInfo = new CompanyInfo()
            {
                CompanyName = companyInfoDto.CompanyName,
                State = State.Active,
                TraderRange = companyInfoDto.TraderRange
            };

            if (_companyService.Exists(companyInfoDto.CompanyName))
            {
                return Fail("公司名称已经存在!");
            }

            _companyService.Add(companyInfo);

            return Success();
        }

        public JsonResult GetCopanyByLikeName(string likeName = "")
        {
            if (likeName.IsNullOrEmpty())
            {
                return Success();
            }

            var companyList = _companyService.GetByLikeName(likeName);

            return Success(companyList);
        }

        public JsonResult GetCompanyById(int id = 0)
        {
            var dto = _companyService.GetById(id);
            return Success(dto);
        }

        public JsonResult CompanyIsExists(string companyFullName)
        {
            return Success(_companyService.Exists(companyFullName));
        }
    }
}