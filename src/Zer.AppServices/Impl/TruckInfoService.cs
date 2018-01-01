using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Zer.Entities;
using Zer.Framework.Extensions;
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

        public TruckInfoDto Edit(TruckInfoDto model)
        {
            throw new NotImplementedException();
        }

        public TruckInfoDto GetByTruckNo(string truckNo)
        {
            return _truckInfoDataService.GetAll()
                .FirstOrDefault(x => x.FrontTruckNo == truckNo)
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
            if (list.IsNullOrEmpty())
            {
                return new List<TruckInfoDto>();
            }

            var truckNoList = list.Select(x => x.FrontTruckNo).ToList();

            var existsTruckList = _truckInfoDataService.GetAll()
                .Where(x => truckNoList.Contains(x.FrontTruckNo)).ToList();

            var waitForRegistList = list.Where(x => existsTruckList.All(y => y.FrontTruckNo != x.FrontTruckNo))
                .ToList();

            var registedList = AddRange(waitForRegistList);

            registedList.AddRange(existsTruckList.Map<TruckInfoDto>().ToList());

            return registedList;
        }

        public TruckInfoDto QueryAfterValidateAndRegist(TruckInfoDto dto)
        {
            var truckInfoEntity = _truckInfoDataService.GetAll().FirstOrDefault(x => x.FrontTruckNo == dto.FrontTruckNo);

            return truckInfoEntity != null 
                ? truckInfoEntity.Map<TruckInfoDto>()
                : _truckInfoDataService.Insert(dto.Map<TruckInfo>()).Map<TruckInfoDto>();
        }
    }
}