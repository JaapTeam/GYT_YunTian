using System;
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

        public LngAllowanceInfoDto GetById(string id)
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
            //var existsList = list.Where(Exists).ToList();
            //var noexistsList = list.Where(x => !Exists(x)).ToList();
            var insertList = _lngAllowanceInfoDataService.AddRange(list.Map<LngAllowanceInfo>()).Map<LngAllowanceInfoDto>().ToList();

            //insertList.AddRange(existsList);
            return insertList;
        }

        public LngAllowanceInfoDto Edit(LngAllowanceInfoDto model)
        {
            return _lngAllowanceInfoDataService.Update(model.Id, x =>
            {
                x.CompanyName = model.CompanyName;
                x.LotId = model.LotId;
                x.TruckNo = model.TruckNo;
                x.EngineId = model.EngineId;
                x.CylinderDefaultId = model.CylinderDefaultId;
                x.CylinderSeconedId = model.CylinderSeconedId;
                x.CreateTime = model.CreateTime;
                x.Status = model.Status;
            }).Map<LngAllowanceInfoDto>();
        }

        public bool Exists(LngAllowanceInfoDto lngAllowanceInfoDto)
        {
            if (lngAllowanceInfoDto.EngineId.Trim().IsNullOrEmpty())
            {
                return _lngAllowanceInfoDataService.GetAll().Any(x => x.TruckNo == lngAllowanceInfoDto.TruckNo);
            }
            
            return _lngAllowanceInfoDataService.GetAll()
                .Any(x => x.TruckNo == lngAllowanceInfoDto.TruckNo || x.EngineId == lngAllowanceInfoDto.EngineId);
        }

        /// <summary>
        /// 检查重复
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<LngAllowanceInfoDto> RepeatedValidate(List<LngAllowanceInfoDto> list)
        {
            var allTruckNoList = list.Select(x => x.TruckNo.Trim()).Distinct().ToList();
            var allEngieIdList = list.Where(x=>!x.EngineId.Trim().IsNullOrEmpty()).Select(x => x.EngineId.Trim()).Distinct().ToList();

            var query = _lngAllowanceInfoDataService.GetAll().Where(x=> x.EngineId!=null || x.EngineId.Trim().Length>0)
                                                             .Where(x=>allTruckNoList.Any(y=>y==x.TruckNo.Trim())
                                                             || allEngieIdList.Any(y=>y==x.EngineId.Trim())).ToList();

            var result = list.Where(x => query.Any(y => y.EngineId.Trim() == x.EngineId.Trim() 
                                                     || y.TruckNo.Trim() == x.TruckNo.Trim())).ToList();

            return result;

            //var repeatedTruckNoList = allTruckNoList.Where(x => list.Count(y => y.TruckNo.Trim() == x) > 1).ToList();
            //var repeatedEngieIdList = allEngieIdList.Where(x => list.Count(y => y.EngineId.Trim() == x) > 1).ToList();

            //var query = list.Where(x => repeatedEngieIdList.Any(y => y == x.EngineId.Trim()));
            //query = query.Where(x => repeatedTruckNoList.Any(y => y == x.TruckNo.Trim()));

        }

        public List<LngAllowanceInfoDto> GetList(LngAllowanceSearchDto searchDto)
        {
            var query = _lngAllowanceInfoDataService.GetAll();

            if (searchDto == null) return query.Map<LngAllowanceInfoDto>().ToList();

            query = Filter(searchDto, query);

            query = query.ToPageQuery(searchDto);

            return query.Map<LngAllowanceInfoDto>().ToList();
        }

        public LngAllowanceInfoDto ChangStatus(string id)
        {
            var infoDto = _lngAllowanceInfoDataService.GetById(id);
            infoDto.CreateTime = DateTime.Now;
            if (infoDto.Status == LngStatus.未补贴)
            {
                infoDto.Status = LngStatus.已补贴;
            }

            return _lngAllowanceInfoDataService.Update(infoDto.Map<LngAllowanceInfo>()).Map<LngAllowanceInfoDto>();
        }

        public void AddDescription(string id, string description)
        {
            _lngAllowanceInfoDataService.Update(id, x => x.Description = description);
        }

        public void ForceImport(List<LngAllowanceInfoDto> list)
        {
            foreach (var item in list)
            {
                item.IsForceImport = true;
            }
            _lngAllowanceInfoDataService.AddRange(list.Map<LngAllowanceInfo>().ToList());
        }

        private IQueryable<LngAllowanceInfo> Filter(LngAllowanceSearchDto searchDto, IQueryable<LngAllowanceInfo> query)
        {
            if (!searchDto.TruckNo.IsNullOrEmpty())
            {
                query = query.Where(x => x.TruckNo.Contains(searchDto.TruckNo.Trim()));
            }

            if (!searchDto.CompanyName.IsNullOrEmpty())
            {
                query = query.Where(x => x.CompanyName.Contains(searchDto.CompanyName.Trim()));
            }

            if (!searchDto.EngineId.IsNullOrEmpty())
            {
                query = query.Where(x => x.EngineId.Contains(searchDto.EngineId.Trim()));
            }

            if (searchDto.IsForceImport.HasValue)
            {
                query = query.Where(x => x.IsForceImport == searchDto.IsForceImport.Value);
            }

            if (searchDto.IsAllowanced.HasValue)
            {
                query = query.Where(x => x.Status == searchDto.IsAllowanced.Value);
            }

            if (searchDto.StartDate.HasValue)
            {
                query = query.Where(x => x.CreateTime >= searchDto.StartDate);
            }

            if (searchDto.EndDate.HasValue)
            {
                query = query.Where(x => x.CreateTime <= searchDto.EndDate);
            }

            return query;
        }
    }
}