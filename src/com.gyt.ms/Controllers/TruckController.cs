using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zer.AppServices;
using Zer.GytDto;
using Zer.Services;

namespace com.gyt.ms.Controllers
{
    public class TruckController : BaseController
    {
        private readonly ITruckInfoService _truckInfoService;

        public TruckController(ITruckInfoService truckInfoService)
        {
            _truckInfoService = truckInfoService;
        }

        public JsonResult GetTruckInfoById(int truckId=0)
        {
            if (truckId == 0)
            {
                return Fail();
            }
            var dto = _truckInfoService.GetById(truckId);

            if (dto == null)
            {
                return Fail("未查询到相关数据！");
            }
            return Success(dto);
        }

        public JsonResult GetTruckInfoListByCompanyId(int companyId=0)
        {
            if (companyId==0)
            {
                return Fail();
            }

            var list = _truckInfoService.GetListByCompanyId(companyId);

            if (list.Count<=0)
            {
                return Fail("未查询到相关数据！");
            }
            return Success(list);
        }

        public JsonResult GetTruckInforByTruckNo(string truckNo)
        {
            var dto = _truckInfoService.GetByTruckNo(truckNo);

            if (dto.Id==0)
            {
                return Fail("未查询到相关数据！");
            }

            return Success(dto);
        }

        //public JsonResult AddRange(List<TruckInfoDto> listInfo)
        //{
        //    ValidataInputString(
        //        listInfo.Select(x => x.CompanyName),
        //        listInfo.Select(x => x.DriverName),
        //        listInfo.Select(x => x.FrontTruckNo),
        //        listInfo.Select(x => x.RearTruckNo));

        //    var result = _truckInfoService.AddRange(listInfo);

        //    return Success(result);
        //}

        public JsonResult Add(TruckInfoDto infoDto)
        {            var result = _truckInfoService.Add(infoDto);

            return Success(result);
        }

        public JsonResult ExistsByTruckNo(string truckNo)
        {
            var result = _truckInfoService.Exists(truckNo);

            return Success(result);
        }
    }
}