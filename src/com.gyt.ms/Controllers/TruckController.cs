using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zer.Services.Company;
using Zer.Services.Truck;
using Zer.Services.Truck.Dto;

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
            var dto = _truckInfoService.GetById(truckId);
            return Success(dto);
        }

        public JsonResult GetTruckInfoListByCompanyId(int companyId=0)
        {
            var list = _truckInfoService.GetListByCompanyId(companyId);
            return Success(list);
        }

        public JsonResult GetTruckInforByTruckNo(string truckNo)
        {
            ValidataInputString(truckNo);
            var dto = _truckInfoService.GetByTruckNo(truckNo);
            return Success(dto);
        }

        public JsonResult AddRange(List<TruckInfoDto> listInfo)
        {
            foreach (TruckInfoDto truckInfoDto in listInfo)
            {
                ValidataInputString(truckInfoDto.ConpanyName,truckInfoDto.DriverName,truckInfoDto.FrontTruckNo,truckInfoDto.RearTruckNo);
            }

            var result = _truckInfoService.AddRange(listInfo);

            return Success(result);
        }

        public JsonResult Add(TruckInfoDto infoDto)
        {
            ValidataInputString(infoDto.ConpanyName, infoDto.DriverName, infoDto.FrontTruckNo, infoDto.RearTruckNo);

            var result = _truckInfoService.Add(infoDto);

            return Success(result);
        }

        public JsonResult ExistsByTruckNo(string truckNo)
        {
            ValidataInputString(truckNo);

            var result = _truckInfoService.TruckNoExists(truckNo);

            return Success(result);
        }
    }
}