using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using com.gyt.ms.Models;
using Zer.AppServices;
using Zer.Entities;
using Zer.Framework.Cache;
using Zer.Framework.Exception;
using Zer.Framework.Extensions;
using Zer.Framework.Mvc.Logs.Attributes;
using Zer.GytDto;
using Zer.GytDto.SearchFilters;
using Zer.GytDto.Users;

namespace com.gyt.ms.Controllers
{
    public class BusinessHandleController : BaseController
    {
        private readonly IPeccancyRecrodService _peccancyRecrodService;
        private readonly IGYTInfoService _gytInfoService;
        private readonly ICompanyService _companyService;
        private readonly ITruckInfoService _truckInfoService;

        public BusinessHandleController(IPeccancyRecrodService peccancyRecrodService, IGYTInfoService gytInfoService, ICompanyService companyService, ITruckInfoService truckInfoService)
        {
            _peccancyRecrodService = peccancyRecrodService;
            _gytInfoService = gytInfoService;
            _companyService = companyService;
            _truckInfoService = truckInfoService;
        }

        // GET: BusinessHandle
        [UserActionLog("天然气车辆业务办理", ActionType.查询)]
        public ActionResult Index()
        {
            return View();
        }

        // GET: BusinessHandle
        [UserActionLog("天然气车辆业务办理", ActionType.查询)]
        public ActionResult Gas()
        {
            ViewBag.ProvinceList = PartString(CacheHelper.GetCache("Province").ToString(), ',');
            ViewBag.CharacterList = PartString(CacheHelper.GetCache("Character").ToString(), ',');
            return View();
        }

        // GET: BusinessHandle
        [UserActionLog("过户车辆业务办理", ActionType.查询)]
        public ActionResult Transfer()
        {
            ViewBag.ProvinceList = PartString(CacheHelper.GetCache("Province").ToString(), ',');
            ViewBag.CharacterList = PartString(CacheHelper.GetCache("Character").ToString(), ',');
            return View();
        }

        // GET: BusinessHandle
        [UserActionLog("已旧换新车辆业务办理", ActionType.查询)]
        public ActionResult New()
        {
            ViewBag.ProvinceList = PartString(CacheHelper.GetCache("Province").ToString(), ',');
            ViewBag.CharacterList = PartString(CacheHelper.GetCache("Character").ToString(), ',');
            return View();
        }

        /// <summary>
        /// 检查申办企业是否有违法记录
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        [UnLog]
        public JsonResult CompanyPeccancyCheck(string companyName)
        {
            var result = _peccancyRecrodService.ExistsCompanyName(companyName);
            if (result)
            {
                return Fail();
            }
            return Success();
        }

        /// <summary>
        /// 已旧换新指标是否被使用
        /// </summary>
        /// <param name="truckNo"></param>
        /// <returns></returns>
        [UnLog]
        public JsonResult TruckRepetitionCheck(string truckNo)
        {
            var result = _gytInfoService.TargetIsUse(truckNo);
            return result ? Fail() : Success();
        }

        public ActionResult Commit(GYTInfoDto dto)
        {
            if (dto.BidCompanyName.IsNullOrEmpty() || dto.BidTruckNo.IsNullOrEmpty())
            {
                throw new CustomException("参数错误，请重新输入"); 
            }

            // TODO: 根据业务类型不同，判断条件不同
            switch (dto.BusinessType)
            {
                case BusinessType.天然气车辆: 
                    
                    break;

                case BusinessType.过户车辆:

                    ValidateAllowBid(dto);
                    break;

                case BusinessType.以旧换新车辆:

                    if (_gytInfoService.TargetIsUse(dto.OriginalTruckNo))
                    {
                        throw new CustomException("已旧换新指标已被使用，不符合办理条件"); 
                    }

                    ValidateAllowBid(dto);

                    break;

                default: throw new CustomException("不正常的业务提交");
            }

            if (_gytInfoService.Exists(dto.BidTruckNo))
            {
                throw new CustomException("申办车牌号已存在办理记录，不符合办理条件", "申办车牌号", dto.BidTruckNo);
            }

            if (_peccancyRecrodService.ExistsCompanyName(dto.BidCompanyName))
            {
                throw new CustomException("申办企业存在超载超限违法记录，不符合办理条件", "公司名称", dto.BidCompanyName);
            }

            if (_peccancyRecrodService.ExistsCompanyName(dto.OriginalCompanyName))
            {
                throw new CustomException("原企业存在超载超限违法记录，不符合办理条件", "公司名称", dto.OriginalCompanyName);
            }

            //检测公司是否存在
            var companyList = new List<CompanyInfoDto>
            {
                new CompanyInfoDto() {CompanyName = dto.BidCompanyName},
                new CompanyInfoDto() {CompanyName = dto.OriginalCompanyName}
            };

            companyList = _companyService.QueryAfterValidateAndRegist(companyList);

            var bidCompanyInfo = companyList.FirstOrDefault(x => x.CompanyName.Trim() == dto.BidCompanyName.Trim());

            dto.BidCompanyId = bidCompanyInfo != null ? bidCompanyInfo.Id : 0;

            if (!dto.OriginalCompanyName.IsNullOrEmpty())
            {
                var originalCompanyInfo = companyList.FirstOrDefault(x => x.CompanyName.Trim() == dto.OriginalCompanyName.Trim());

                dto.OriginalCompanyId = originalCompanyInfo != null ? originalCompanyInfo.Id : 0;
            }

            // 检测车辆信息
            var waitForRegistTruckInfo = new List<TruckInfoDto>
            {
                new TruckInfoDto(){FrontTruckNo =  dto.BidTruckNo,CompanyId = dto.BidCompanyId,CompanyName = dto.BidCompanyName}
            };

            if (dto.BusinessType.ToInt() > 0)
            {
                waitForRegistTruckInfo.Add(new TruckInfoDto
                {
                    FrontTruckNo = dto.OriginalTruckNo,
                    CompanyId = dto.OriginalCompanyId,
                    CompanyName = dto.OriginalCompanyName
                });
            }

            _truckInfoService.QueryAfterValidateAndRegist(waitForRegistTruckInfo);

            var userInfoDto = GetValueFromSession<UserInfoDto>("UserInfo");

            dto.BidName = userInfoDto.UserName;
            dto.BidDisplayName = userInfoDto.DisplayName;
            dto.Status = BusinessState.初审通过;
            dto.BidDate = DateTime.Now;

            var result = _gytInfoService.Add(dto);

            if (result == null)
            {
                throw new CustomException("数据保存失败，请重试.");
            }

            return RedirectToAction("Index", "GYTInfo");
        }

        private void ValidateAllowBid(GYTInfoDto dto)
        {
            if ( dto.OriginalCompanyName.IsNullOrEmpty() || !_companyService.Exists(dto.OriginalCompanyName))
            {
                throw new CustomException("原公司信息不存在", "公司名称", dto.OriginalCompanyName);
            }

            if (dto.OriginalTruckNo.IsNullOrEmpty() || !_truckInfoService.Exists(dto.OriginalTruckNo))
            {
                throw new CustomException("原车辆信息不存在", "车牌号", dto.OriginalTruckNo);
            }
        }

        /// <summary>
        /// 检查是否重复办理
        /// </summary>
        /// <param name="truckNo"></param>
        /// <returns></returns>
        [UnLog]
        public JsonResult TruckNoExistsHandle(string truckNo)
        {
            var isExists = _gytInfoService.Exists(truckNo);
            if (isExists)
            {
                return Fail();
            }

            return Success();
        }

        private List<string> PartString(string str,char mark)
        {
            if (str.Length<=0)
            {
                return new List<string>();
            }

            if (mark.ToString().IsNullOrEmpty())
            {
                return new List<string>();
            }
            var stringList = str.Split(mark).ToList();
            return stringList;
        }
    }
}