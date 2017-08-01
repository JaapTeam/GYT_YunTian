using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Zer.Entities;
using Zer.Framework.Entities;
using Zer.Framework.Extensions;
using Zer.GytDto;
using Zer.Services;

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
            ViewBag.ActiveId = 0;
            var dto = _companyService.GetById(1);
            return View(dto);
        }

        public ActionResult DemoForAdd()
        {
            var dto = new CompanyInfoDto()
            {
                CompanyName = "Zer software .CLT",
                TraderRange = "Software devlopement"
            };

            _companyService.Add(Mapper.Map<CompanyInfo>(dto));

            return RedirectToAction("Index");
        }

        public JsonResult AddCompany(CompanyInfoDto companyInfoDto)
        {
            if (companyInfoDto == null) return Fail();

            ValidataInputString(companyInfoDto.CompanyName, companyInfoDto.TraderRange);

            var companyInfo = Mapper.Map<CompanyInfo>(companyInfoDto);

            if (_companyService.Exists(companyInfo.CompanyName))
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