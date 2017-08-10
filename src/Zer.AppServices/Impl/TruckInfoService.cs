using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Zer.Entities;
using Zer.GytDataService;
using Zer.GytDto;
using Zer.GytDto.Extensions;

namespace Zer.AppServices.Impl
{
    public class TruckInfoService : ITruckInfoService
    {
        private readonly ITruckInfoDataService _truckInfoDataService;

        public TruckInfoService(ITruckInfoDataService truckInfoDataService)
        {
            _truckInfoDataService = truckInfoDataService;
        }

        public TruckInfoDto GetById(int id)
        {
            return _truckInfoDataService.GetById(id).Map<TruckInfoDto>();
        }

        public List<TruckInfoDto> GetAll()
        {
            return _truckInfoDataService.GetAll().Map<TruckInfoDto>().ToList();
        }

        public TruckInfoDto Add(TruckInfoDto model)
        {
            var entity = model.Map<TruckInfo>();
            return _truckInfoDataService.Insert(entity).Map<TruckInfoDto>();
        }

        /// <summary>
        /// 进行批量新增时，请检查是否在数据库中已经存在数据
        /// </summary>
        public List<TruckInfoDto> AddRange(List<TruckInfoDto> list)
        {
            return _truckInfoDataService.AddRange(list.Map<TruckInfo>()).Map<TruckInfoDto>().ToList();
        }

        public TruckInfoDto GetByTruckNo(string truckNo)
        {
            return _truckInfoDataService.GetAll()
                .Single(x => x.FrontTruckNo == truckNo || x.BehindTruckNo == truckNo)
                .Map<TruckInfoDto>();
        }

        public List<TruckInfoDto> GetListByCompanyId(int id)
        {
            return _truckInfoDataService.GetAll().Where(x => x.CompanyId == id).Map<TruckInfoDto>().ToList();
        }

        public bool Exists(string truckNo)
        {
            return _truckInfoDataService.GetAll().Any(x => x.FrontTruckNo == truckNo || x.BehindTruckNo == truckNo);
        }

        public List<TruckInfoDto> QueryAfterValidateAndRegist(List<TruckInfoDto> list)
        {
            var waitForRegistList = list.Where(x => !(Exists(x.FrontTruckNo) || Exists(x.BehindTruckNo))).ToList();

            return AddRange(waitForRegistList);
        }

    }
}