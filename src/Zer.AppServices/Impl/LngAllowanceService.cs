using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Zer.Entities;
using Zer.Framework.Dto;
using Zer.Framework.Extensions;
using Zer.GytDataService;
using Zer.GytDto;
using Zer.GytDto.Extensions;
using Zer.GytDto.SearchFilters;

namespace Zer.AppServices.Impl
{
    public class LngAllowanceService : ILngAllowanceService
    {
        private readonly ILngAllowanceInfoDataService _lngAllowanceInfoDataService;

        public LngAllowanceService(ILngAllowanceInfoDataService lngAllowanceInfoDataService)
        {
            _lngAllowanceInfoDataService = lngAllowanceInfoDataService;
        }

        public LngAllowanceInfoDto GetById(int id)
        {
            return Mapper.Map<LngAllowanceInfoDto>(_lngAllowanceInfoDataService.GetById(id));
        }

        public List<LngAllowanceInfoDto> GetAll()
        {
            return _lngAllowanceInfoDataService.GetAll().Map<LngAllowanceInfoDto>().ToList();
        }

        public LngAllowanceInfoDto Add(LngAllowanceInfoDto model)
        {
            return _lngAllowanceInfoDataService.Insert(model.Map<LngAllowanceInfo>()).Map<LngAllowanceInfoDto>();
        }

        public List<LngAllowanceInfoDto> AddRange(List<LngAllowanceInfoDto> list)
        {
            var existsList = list.Where(Exists).ToList();
            var noexistsList = list.Where(x => !Exists(x)).ToList();
            var insertList = _lngAllowanceInfoDataService.AddRange(noexistsList.Map<LngAllowanceInfo>()).Map<LngAllowanceInfoDto>().ToList();

            insertList.AddRange(existsList);
            return insertList;
        }

        public List<LngAllowanceInfoDto> GetList(FilterMatchInputDto filterMatchInput)
        {
            throw new System.NotImplementedException();
        }

        public List<LngAllowanceInfoDto> GetList(int[] idList)
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(LngAllowanceInfoDto lngAllowanceInfoDto)
        {
            return _lngAllowanceInfoDataService.GetAll()
                .Any(x => x.TruckNo == lngAllowanceInfoDto.TruckNo ||
                          x.CylinderDefaultId == lngAllowanceInfoDto.CylinderDefaultId);
        }

        public List<LngAllowanceInfoDto> GetList(LngAllowanceSearchDto searchDto)
        {
            var query = _lngAllowanceInfoDataService.GetAll();

            if (searchDto == null) return query.Map<LngAllowanceInfoDto>().ToList();

            query = Filter(searchDto, query);

            query = query.ToPageQuery(searchDto); 

            return query.Map<LngAllowanceInfoDto>().ToList();
        }

        private IQueryable<LngAllowanceInfo> Filter(LngAllowanceSearchDto searchDto, IQueryable<LngAllowanceInfo> query)
        {
            if (!searchDto.TruckNo.IsNullOrEmpty())
            {
                query = query.Where(x => x.TruckNo.Contains(searchDto.TruckNo));
            }

            if (!searchDto.EngineId.IsNullOrEmpty())
            {
                query = query.Where(x => x.EngineId.Contains(searchDto.EngineId));
            }

            if (searchDto.IsAllowanced.HasValue)
            {
                //query = query.Where(x=>x.)
            }
            return query;
        }
    }
}