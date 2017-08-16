using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Zer.Entities;
using Zer.Framework.Dto;
using Zer.Framework.Exception;
using Zer.Framework.Extensions;
using Zer.GytDataService;
using Zer.GytDto;
using Zer.GytDto.Extensions;
using Zer.GytDto.SearchFilters;

namespace Zer.AppServices.Impl
{
    public class GYTInfoService : IGYTInfoService
    {
        private readonly IGYTInfoDataService _gytInfoDataService;

        public GYTInfoService(IGYTInfoDataService gytInfoDataService)
        {
            _gytInfoDataService = gytInfoDataService;
        }

        public GYTInfoDto GetById(int id)
        {
            return _gytInfoDataService.GetById(id).Map<GYTInfoDto>();
        }

        public List<GYTInfoDto> GetAll()
        {
            return _gytInfoDataService.GetAll().Map<GYTInfoDto>().ToList();
        }

        public GYTInfoDto Add(GYTInfoDto model)
        {
            if (Exists(model.BidTruckNo))
            {
                throw new CustomException(
                    "公司信息已经存在",
                    new Dictionary<string, string>()
                    {
                        {"CompanyName", model.BidTruckNo}
                    });
            }

            var gtyInfoDto = model.Map<GYTInfo>();
            return _gytInfoDataService.Insert(gtyInfoDto).Map<GYTInfoDto>();
        }

        public List<GYTInfoDto> AddRange(List<GYTInfoDto> list)
        {
            var entities = list.Map<GYTInfo>();
            return _gytInfoDataService.AddRange(entities).Map<GYTInfoDto>().ToList();
        }

        public bool Exists(string bidTruckNo)
        {
            return _gytInfoDataService.GetAll().Any(x => x.BidTruckNo == bidTruckNo.Trim());
        }

        public List<GYTInfoDto> GetVerifyList(GYTInfoSearchDto searchDto)
        {
            var query = _gytInfoDataService.GetAll().Where(x=>x.Status==BusinessState.已审核);

            if (searchDto == null) return query.Map<GYTInfoDto>().ToList();

            query = Filter(searchDto, query);

            query = query.ToPageQuery(searchDto);

            return query.Map<GYTInfoDto>().ToList();
        }

        public List<GYTInfoDto> GetList(GYTInfoSearchDto searchDto)
        {
            var query = _gytInfoDataService.GetAll();

            if (searchDto == null) return query.Map<GYTInfoDto>().ToList();

            query = Filter(searchDto, query);

            query = query.ToPageQuery(searchDto);

            return query.Map<GYTInfoDto>().ToList();
        }

        private IQueryable<GYTInfo> Filter(GYTInfoSearchDto searchDto, IQueryable<GYTInfo> query)
        {
            if (!searchDto.CompanyName.IsNullOrEmpty())
            {
                query = query.Where(x => x.BidCompanyName.Contains(searchDto.CompanyName) || x.OriginalCompanyName.Contains(searchDto.CompanyName));
            }

            if (!searchDto.TruckNo.IsNullOrEmpty())
            {
                query = query.Where(x => x.BidTruckNo == searchDto.TruckNo || x.OriginalTruckNo == searchDto.TruckNo);
            }

            if (searchDto.Status != 0)
            {
                query = query.Where(x => x.Status == searchDto.Status);
            }

            if (searchDto.StratDate!=null)
            {
                query = query.Where(x => x.BidDate >= searchDto.StratDate);
            }

            if (searchDto.StratDate != null)
            {
                query = query.Where(x => x.BidDate <= searchDto.EndDate);
            }

            return query;
        }

        public bool TargetIsUse(string truckNo)
        {
            return
                _gytInfoDataService.GetAll()
                    .Any(x => x.OriginalTruckNo == truckNo && x.BusinessType == BusinessType.以旧换新车辆);
        }

        public GYTInfoDto Verify(int infoId)
        {
            var gytInfoDto = _gytInfoDataService.GetById(infoId);

            if (gytInfoDto.Status==BusinessState.已通过)
            {
                gytInfoDto.Status = BusinessState.已审核;
            }

            return _gytInfoDataService.Update(gytInfoDto.Map<GYTInfo>()).Map<GYTInfoDto>();
        }
    }
}
