using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zer.AppServices;
using Zer.Entities;
using Zer.Framework.Cache;
using Zer.Framework.Exception;
using Zer.Framework.Extensions;
using Zer.Framework.Mvc.Logs.Attributes;
using Zer.GytDto;
using Zer.GytDto.SearchFilters;

namespace com.gyt.ms.Controllers
{
    [RoutePrefix("")]
    public class HomeController : BaseController
    {
        private readonly IGYTInfoService _gytInfoService;
        private readonly ICompanyService _companyService;
        private readonly IPeccancyRecrodService _peccancyRecrodService;

        public HomeController(
            IGYTInfoService gytInfoService,
            ICompanyService companyService,
            IPeccancyRecrodService peccancyRecrodService)
        {
            _gytInfoService = gytInfoService;
            _companyService = companyService;
            _peccancyRecrodService = peccancyRecrodService;
        }

        [UserActionLog("加载首页",ActionType.查询)]
        [Route("")]
        public ActionResult Index()
        {
            var homePageSize = (int)CacheHelper.GetCache("HomePageSize");

            var searchDto =new GYTInfoSearchDto()
            {
                PageSize = homePageSize,
                PageIndex = 1
            };

            var gytInfoWaitCheckList = _gytInfoService.GetList(searchDto);

            ViewBag.GytInfoWaitCheckList = gytInfoWaitCheckList;


            var filter = new PeccancyWithCompanySearchDto
            {
                PageSize = homePageSize
            };

            ViewBag.CompanyList = _peccancyRecrodService.GetPeccancyGroupByCompany(filter);

            return View();
        }

        [UnValidateLogin]
        [UnLog]
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }
    }
}