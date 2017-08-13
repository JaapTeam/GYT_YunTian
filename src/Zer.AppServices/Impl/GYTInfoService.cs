using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Zer.Entities;
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

        public List<GYTInfoDto> GetList(GYTInfoSearchDto searchDto)
        {
            var query = _gytInfoDataService.GetAll();

            if (searchDto.CompanyId!=0)
            {
                query = query.Where(x => x.BidCompanyId == searchDto.CompanyId || x.OriginalCompanyId == searchDto.CompanyId);
            }

            if (!searchDto.TruckNo.IsNullOrEmpty())
            {
                query = query.Where(x => x.BidTruckNo == searchDto.TruckNo||x.OriginalTruckNo==searchDto.TruckNo);
            }

            if (searchDto.Status != 0)
            {
                query = query.Where(x => x.Status == searchDto.Status);
            }
            return query.Map<GYTInfoDto>().ToList();
        }

        public bool TargetIsUse(string truckNo)
        {
            return
                _gytInfoDataService.GetAll()
                    .Any(x => x.OriginalTruckNo == truckNo && x.BusinessType == BusinessType.以旧换新车辆);
        }
    }
}
