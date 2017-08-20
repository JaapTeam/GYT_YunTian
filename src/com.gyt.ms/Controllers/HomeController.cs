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
        public ActionResult Index()
        {
            var homePageSize = (int)CacheHelper.GetCache("HomePageSize");

            var searchDto =new GYTInfoSearchDto()
            {
                PageSize = homePageSize,
                PageIndex = 1,
                Status =  BusinessState.初审通过
            };

            var gytInfoWaitCheckList = _gytInfoService.GetList(searchDto);

            ViewBag.GytInfoWaitCheckList = gytInfoWaitCheckList;


            var companyIdList = _peccancyRecrodService.GetAll().GroupBy(x => x.CompanyId, x => x)
                .Where(x => x.Any())
                .OrderByDescending(x => x.Count())
                .Take(homePageSize)
                .Select(x => new { CompanyId = x.Key, Count = x.Count() }).OrderByDescending(x => x.Count).ToList();

            if (companyIdList.IsNullOrEmpty())
            {
                return View();
            }

            CompanySearchDto companyFilter = new CompanySearchDto()
            {
                PageIndex = 1,
                PageSize = homePageSize,
                CompanyIdList = companyIdList.Select(x => x.CompanyId).Distinct().ToList()
            };

            var companyList = _companyService.GetWithPeccancyRecored(companyFilter);

            if (companyList.IsNullOrEmpty())
            {
                return View();
            }

            companyList = companyList.Join(companyIdList, x => x.Id, x => x.CompanyId, (x, id) => new CompanyInfoDto()
            {
                CompanyName = x.CompanyName,
                Id = x.Id,
                PeccancyRecordCount = id.Count,
                TraderRange = (!x.TraderRange.IsNullOrEmpty()) && x.TraderRange.Length>9? x.TraderRange.Substring(0,10)+ "..." : x.TraderRange
            }).OrderByDescending(x=>x.PeccancyRecordCount).ToList();

            ViewBag.CompanyList = companyList;

            return View();
        }

        [UnValidateLogin]
        [UnLog]
        public ActionResult Login()
        {
            return View();
        }

        ////public ActionResult About()
        ////{
        ////    throw new CustomException("define a new exception!",
        ////        new Dictionary<string, string>()
        ////        {
        ////            {"username","paul"}
        ////        });
            
        ////}
    }
}