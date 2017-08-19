using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using com.gyt.ms.Models;
using Zer.AppServices;
using Zer.Entities;
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
            return View();
        }

        // GET: BusinessHandle
        [UserActionLog("过户车辆业务办理", ActionType.查询)]
        public ActionResult Transfer()
        {
            return View();
        }

        // GET: BusinessHandle
        [UserActionLog("已旧换新车辆业务办理", ActionType.查询)]
        public ActionResult New()
        {
            return View();
        }

        public JsonResult Html(BusinessType businessType = BusinessType.天然气车辆)
        {
            StringBuilder sb = new StringBuilder();

            if (businessType == BusinessType.天然气车辆)
            {
                #region 天然气车辆HTML

                sb.Append("<div id='gas'>");
                sb.Append("<div class='form-group'>");
                sb.Append(
                    "<label class='col-sm-3 control-label no-padding-right' for='input_CompanyName'> 申办企业名称: </label>");
                sb.Append("<div class='col-sm-9'>");
                sb.Append(
                    "<input type='text' id='input_CompanyName' placeholder='请输入企业名称' class='col-xs-10 col-sm-5'>");
                sb.Append("<span class='help-inline col-xs-12 col-sm-7'>");
                sb.Append("</span>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("<div class='space-4'></div>");
                sb.Append("<div class='form-group'>");
                sb.Append(
                    "<label class='col-sm-3 control-label no-padding-right' for='input_bidTruckNo'> 申办车牌号: </label>");
                sb.Append("<div class='col-sm-9'>");
                sb.Append(
                    "<input type='text' id='input_bidTruckNo' placeholder='请输入申办车牌号' class='col-xs-10 col-sm-5'>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("<div class='space-4'></div>");
                sb.Append("<div class='form-group'>");
                sb.Append("<label class='col-sm-3 control-label no-padding-right'> 超载超限违规情况: </label>");
                sb.Append("<div class='col-sm-9'>");
                sb.Append("<span class='label label-lg label-success' id='_sp_Illegal'>无违规</span>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("<div class='form-group'>");
                sb.Append("<label class='col-sm-3 control-label no-padding-right'> 车辆年审情况: </label>");
                sb.Append("<div class='col-sm-9'>");
                sb.Append("<div class='control-group' style='margin-top: -5px; margin-left: -20px'>");
                sb.Append("<div class='radio' id='radio_Verify'>");
                sb.Append("<label>");
                sb.Append("<input type='radio' class='ace' value='true' checked='checked' name='lblAnnual'>");
                sb.Append("<span class='lbl'> 已年审 </span>");
                sb.Append("</label>");
                sb.Append("<label style='margin-left: 20px'>");
                sb.Append("<input type='radio' class='ace' value='false' name='lblAnnual'>");
                sb.Append("<span class='lbl'> 未年审 </span>");
                sb.Append("</label>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                #endregion

            }
            else if (businessType == BusinessType.过户车辆)
            {
                #region 过户车辆

                sb.Append("<div id='new'>");
                sb.Append("<div class='form-group'>");
                sb.Append(
                    "<label class='col-sm-3 control-label no-padding-right' for='input_CompanyName'> 申办企业名称: </label>");
                sb.Append("<div class='col-sm-9'>");
                sb.Append("<input type='text' id='input_CompanyName' placeholder='请输入企业名称' class='col-xs-10 col-sm-5'>");
                sb.Append("<span class='help-inline col-xs-12 col-sm-7'></span></div> </div>");
                sb.Append("<div class='space-4'></div>");
                sb.Append("<div class='form-group'>");
                sb.Append(
                    "<label class='col-sm-3 control-label no-padding-right' for='input_bidTruckNo'> 申办车牌号: </label>");

                sb.Append("<div class='col-sm-9'>");
                sb.Append("<input type='text' id='input_bidTruckNo' placeholder='请输入申办车牌号' class='col-xs-10 col-sm-5'>");
                sb.Append("</div></div>");

                sb.Append("<div class='space-4'></div>");

                sb.Append("<div class='form-group'>");
                sb.Append("<label class='col-sm-3 control-label no-padding-right'> 超载超限违规情况: </label>");
                sb.Append("<div class='col-sm-9'>");
                sb.Append("<span class='label label-lg label-success' id='_sp_Illegal'>无违规</span></div></div>");


                sb.Append("<div class='form-group'>");
                sb.Append("<label class='col-sm-3 control-label no-padding-right'> 港运通注销状态: </label>");
                sb.Append("<div class='col-sm-9'>");
                sb.Append("<div class='control-group' style='margin-top: -5px;margin-left: -20px'>");
                sb.Append("<div class='radio' id='radio_GCancel'><label>");

                sb.Append("<input type='radio' class='ace' checked='checked' name='lblCancel' value='true'>");
                sb.Append("<span class='lbl'> 已注销 </span></label>");

                sb.Append("<label style='margin-left: 20px'>");
                sb.Append("<input type='radio' class='ace' name='lblCancel' value='false'>");
                sb.Append("<span class='lbl'> 未注销 </span></label></div> </div> </div></div>");
                sb.Append("<div class='form-group'>");
                sb.Append("<label class='col-sm-3 control-label no-padding-right'> 车辆年审情况: </label>");
                sb.Append("<div class='col-sm-9'>");
                sb.Append(" <div class='control-group' style='margin-top: -5px;margin-left: -20px'>");
                sb.Append("<div class='radio' id='radio_Verify'><label>");

                sb.Append("<input type='radio' class='ace' checked='checked' name='lblAnnual' value='true'>");
                sb.Append("<span class=' lbl'> 已年审 </span></label>");
                sb.Append("<label style='margin-left: 20px'>");
                sb.Append("<input type='radio' class='ace' name='lblAnnual' value='false'>");
                sb.Append("<span class='lbl'> 未年审 </span> </label> </div> </div> </div></div></div>");

                #endregion
        
            }
            else if (businessType == BusinessType.以旧换新车辆)
            {
                #region 以旧换新车辆HTML

                sb.Append("<div id='transfer'>");
                sb.Append("<div class='form-group'>");
                sb.Append(
                    "<label class='col-sm-3 control-label no-padding-right' for='input_CompanyName'> 申办企业名称: </label>");

                sb.Append("<div class='col-sm-9'>");
                sb.Append("<input type='text' id='input_CompanyName' placeholder='请输入企业名称' class='col-xs-10 col-sm-5'>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("<div class='space-4'></div>");
                sb.Append("<div class='form-group'>");
                sb.Append(
                    "<label class='col-sm-3 control-label no-padding-right' for='input_bidTruckNo'> 申办车牌号: </label>");
                sb.Append("<div class='col-sm-9'>");
                sb.Append(
                    "<input type='text' id='input_bidTruckNo' placeholder='请输入申办车牌号' class='col-xs-10 col-sm-5'>");
                sb.Append("</div>");
                sb.Append("</div>");

                sb.Append("<div class='space-4'></div>");

                sb.Append("<div class='form-group'>");
                sb.Append(
                    "<label class='col-sm-3 control-label no-padding-right' for='input_oldTruckno'> 旧车牌号: </label>");

                sb.Append("<div class='col-sm-9'>");
                sb.Append("<input type='text' id='input_oldTruckno' placeholder='请输入旧车牌号' class='col-xs-10 col-sm-5'>");
                sb.Append("<span class='help-inline col-xs-12 col-sm-7'>");
                sb.Append("</span>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("<div class='space-4'></div>");

                sb.Append("<div class='form-group'>");
                sb.Append("<label class='col-sm-3 control-label no-padding-right'> 超载超限违规情况: </label>");
                sb.Append("<div class='col-sm-9'>");
                sb.Append("<span class='label label-lg label-success' id='_sp_Illegal'>无违规</span>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("<div class='form-group'>");
                sb.Append("<label class='col-sm-3 control-label no-padding-right'> 指标无重复使用: </label>");
                sb.Append("<div class='col-sm-9'>");
                sb.Append("<span class='label label-lg label-success' id='_sp_quota'>无重复使用</span>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("<div class='form-group'>");
                sb.Append("<label class='col-sm-3 control-label no-padding-right'> 运营证注销状态:</label>");
                sb.Append("<div class='col-sm-9'>");
                sb.Append("<div class='control-group' style='margin-top: -5px;margin-left: -20px'>");
                sb.Append("<div class='radio' id='radio_Cancel'>");
                sb.Append("<label>");
                sb.Append("<input type='radio' class='ace' checked='checked' name='lblCancel' value='true'>");
                sb.Append("<span class='lbl'> 已注销 </span>");
                sb.Append("</label>");
                sb.Append("<label style='margin-left: 20px'>");
                sb.Append("<input type='radio' class='ace' name='lblCancel' value='false'>");
                sb.Append("<span class='lbl'> 未注销 </span>");
                sb.Append("</label>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("<div class='form-group'>");
                sb.Append("<label class='col-sm-3 control-label no-padding-right'> 运营证存在过户记录: </label>");
                sb.Append("<div class='col-sm-9'>");
                sb.Append("<div class='control-group' style='margin-top: -5px;margin-left: -20px'>");
                sb.Append("<div class='radio' id='radioTransfer'>");
                sb.Append("<label>");
                sb.Append("<input type='radio' class='ace' checked='checked' name='lblTransfer' value='true'>");
                sb.Append("<span class='lbl'> 不存在 </span>");
                sb.Append("</label>");
                sb.Append("<label style='margin-left: 20px'>");
                sb.Append("<input type='radio' class='ace' name='lblTransfer' value='false'>");
                sb.Append("<span class='lbl'> 存在 </span>");
                sb.Append("</label>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("<div class='form-group'>");
                sb.Append("<label class='col-sm-3 control-label no-padding-right'> 过户前任意一手港运通状态: </label>");
                sb.Append("<div class='col-sm-9'>");
                sb.Append("<div class='control-group' style='margin-top: -5px;margin-left: -20px'>");
                sb.Append("<div class='radio' id='radioState'>");
                sb.Append("<label>");
                sb.Append("<input type='radio' class='ace' checked='checked' name='lblState' value='true'>");
                sb.Append("<span class='lbl'> 已注销 </span>");
                sb.Append("</label>");
                sb.Append("<label style='margin-left: 20px'>");
                sb.Append("<input type='radio' class='ace' name='lblState' value='false'>");
                sb.Append("<span class=' lbl'> 未注销 </span>");
                sb.Append("</label>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("<div class='form-group'>");
                sb.Append("<label class='col-sm-3 control-label no-padding-right'> 报废车回收与系统信息一致: </label>");
                sb.Append("<div class='col-sm-9'>");
                sb.Append("<div class='control-group' style='margin-top: -5px;margin-left: -20px'>");
                sb.Append("<div class='radio' id='radioInfo'>");
                sb.Append("<label>");
                sb.Append("<input name='lblInfo' type='radio' class='ace' checked='checked' value='true'>");
                sb.Append("<span class='lbl'> 一致 </span>");
                sb.Append("</label>");
                sb.Append("<label style='margin-left: 20px'>");
                sb.Append("<input name='lblInfo' type='radio' class='ace' value='false'>");
                sb.Append("<span class='lbl'> 不一致 </span>");
                sb.Append("</label>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");

                #endregion
            }

            return Success(null,sb.ToString());
        }
        

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

        [UnLog]
        public JsonResult TruckRepetitionCheck(string truckNo)
        {
            var result = _gytInfoService.TargetIsUse(truckNo);
            return result ? Fail() : Success();
        }

        public ActionResult Commit(GYTInfoDto dto)
        {
            // TODO: 根据业务类型不同，判断条件不同
            switch (dto.BusinessType)
            {
                case BusinessType.天然气车辆: 
                    
                    break;

                case BusinessType.过户车辆: 

                case BusinessType.以旧换新车辆:

                    if (!_companyService.Exists(dto.OriginalCompanyName))
                    {
                        throw new CustomException("原公司信息不存在","公司名称",dto.OriginalCompanyName);
                    }

                    if (!_truckInfoService.Exists(dto.OriginalTruckNo))
                    {
                        throw new CustomException("原车辆信息不存在", "车牌号", dto.OriginalTruckNo);
                    }

                    break;

                default: throw new CustomException("不正常的业务提交");
            }

            if (!_peccancyRecrodService.ExistsCompanyName(dto.BidCompanyName))
            {
                throw new CustomException("申办企业存在超载超限违法记录，不符合办理条件", "公司名称", dto.BidCompanyName);
            }

            if (!_peccancyRecrodService.ExistsCompanyName(dto.OriginalCompanyName))
            {
                throw new CustomException("申办企业存在超载超限违法记录，不符合办理条件", "公司名称", dto.OriginalCompanyName);
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

            var originalCompanyInfo = companyList.FirstOrDefault(x => x.CompanyName.Trim() == dto.OriginalCompanyName.Trim());

            dto.OriginalCompanyId = originalCompanyInfo != null ? originalCompanyInfo.Id : 0;


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
    }
}