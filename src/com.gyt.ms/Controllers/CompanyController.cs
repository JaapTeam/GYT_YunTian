using System;
using System.Linq;
using System.Web.Mvc;
using Zer.Framework.Extensions;
using Zer.Services.Company;

namespace com.gyt.ms.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
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

        public JsonResult GetCompanyInfoById(int id = 0)
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