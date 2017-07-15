using System;
using System.Linq;
using System.Web.Mvc;
using Zer.Entities;
using Zer.Framework.Extensions;
using Zer.Services.Company;
using Zer.Services.Company.Dto;

namespace com.gyt.ms.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public JsonResult AddCompany(CompanyInfoDto companyInfoDto)
        {
            ValidataInputString(companyInfoDto.CompanyName, companyInfoDto.TraderRange);

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
            ValidataInputString(likeName);

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
            ValidataInputString(companyFullName);
            return Success(_companyService.Exists(companyFullName));
        }
    }
}