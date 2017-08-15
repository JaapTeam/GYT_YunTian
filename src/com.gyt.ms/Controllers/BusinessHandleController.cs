using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using com.gyt.ms.Models;
using Zer.AppServices;
using Zer.Entities;
using Zer.Framework.Mvc.Logs.Attributes;
using Zer.GytDto;
using Zer.GytDto.SearchFilters;

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
        public ActionResult Index(int activeId = 1)
        {
            ViewBag.ActiveId = activeId;
            
            return View();
        }

        public JsonResult Html(BusinessType businessType = BusinessType.天然气车辆, int activeId = 1)
        {
            ViewBag.ActiveId = activeId;
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

        public JsonResult SuccessInfo(string bidCompanyName="", string bidTruckNo="", string oldTruckno="",
           bool isAnnual = true, bool isOperationCancel = true, bool isTransferRecrod=true,bool isGytStatus=true,
           bool isGytCancel = true, bool isConsistentInfo = true, BusinessType businessType = BusinessType.天然气车辆, int activeId = 1)
        {
            ViewBag.ActiveId = activeId;

            if (bidCompanyName == "")
            {
                return Fail("申报企业名不能为空！");
            }

            if (bidTruckNo == "")
            {
                return Fail("申报车牌号不能为空！");
            }

            if (oldTruckno == "" && businessType == BusinessType.以旧换新车辆)
            {
                return Fail("旧车牌号不能为空！");
            }

            //是否重复申办
            var isExists = _gytInfoService.Exists(bidTruckNo);
            if (isExists)
            {
                return Fail();
            }

            //有无违规记录
            var isPeccancy = _peccancyRecrodService.ExistsCompanyName(bidCompanyName);
            var targetIsUse = _gytInfoService.TargetIsUse(oldTruckno);

            var gtyInfoDto = new GYTInfoDto();
            gtyInfoDto.BidDate = DateTime.Now;
            gtyInfoDto.BidName = CurrentUser.DisplayName;
            gtyInfoDto.BusinessType = businessType;


            #region 检查公司是否存在，不存在新增，并将公司信息添加到办理信息
            var queryAfterValidateAndRegist =
                _companyService.QueryAfterValidateAndRegist(new List<CompanyInfoDto>
                {
                    new CompanyInfoDto {CompanyName = bidCompanyName}
                });
            var firstOrDefault = queryAfterValidateAndRegist.FirstOrDefault();
            if (firstOrDefault != null)
            {
                gtyInfoDto.BidCompanyId = firstOrDefault.Id;
                gtyInfoDto.BidCompanyName = firstOrDefault.CompanyName;
            }
            else
            {
                var companyInfoDto = _companyService.GetByLikeName(bidCompanyName);
                gtyInfoDto.BidCompanyId = companyInfoDto.FirstOrDefault().Id;
                gtyInfoDto.BidCompanyName = companyInfoDto.FirstOrDefault().CompanyName;
            }
            #endregion

            #region  检查车牌是否存在，不存在新增，并将公司信息添加到办理信息
            if (_truckInfoService.Exists(bidTruckNo))
            {
                gtyInfoDto.BidTruckNo = bidTruckNo;
            }
            else
            {
                _truckInfoService.Add(new TruckInfoDto
                {
                    FrontTruckNo = bidTruckNo,
                    CompanyId = gtyInfoDto.BidCompanyId,
                    CompanyName = gtyInfoDto.BidCompanyName
                });

                gtyInfoDto.BidTruckNo = bidTruckNo;
            }

            #endregion

            HandleDataDto handleDataDto = new HandleDataDto();

            #region 判断审查条件
            if (isAnnual && isOperationCancel && isTransferRecrod && isGytStatus && isGytCancel && isConsistentInfo && !isPeccancy && !targetIsUse)
            {
                gtyInfoDto.Status = BusinessState.已通过;
                handleDataDto.Result = true;
                _gytInfoService.Add(gtyInfoDto);
            }
            else
            {
                handleDataDto.Result = false;
            }
            
            #endregion

            handleDataDto.BidCompanyName = bidCompanyName;
            handleDataDto.BidTruckNo = bidTruckNo;
            handleDataDto.BusinessType = businessType;
            handleDataDto.IsAnnual = isAnnual;
            handleDataDto.IsConsistentInfo = isConsistentInfo;
            handleDataDto.IsGytCancel = isGytCancel;
            handleDataDto.IsGytStatus = isGytStatus;
            handleDataDto.IsPeccancy = isPeccancy;
            handleDataDto.OldTruckNo = oldTruckno;
            handleDataDto.IsTransferRecrod = isTransferRecrod;
            handleDataDto.TargetIsUse = targetIsUse;

            return Success(handleDataDto,"success");
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
            if (result)
            {
                return Fail();
            }
            return Success();
        }
    }
}