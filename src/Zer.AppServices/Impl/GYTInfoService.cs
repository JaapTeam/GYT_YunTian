using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public GYTInfoDto GetById(string id)
        {
            return _gytInfoDataService.GetById(id).Map<GYTInfoDto>();
        }

        public List<GYTInfoDto> GetAll()
        {
            return _gytInfoDataService.GetAll().Map<GYTInfoDto>().ToList();
        }

        public GYTInfoDto Add(GYTInfoDto model)
        {
            var gtyInfoDto = model.Map<GYTInfo>();
            return _gytInfoDataService.Insert(gtyInfoDto).Map<GYTInfoDto>();
        }

        public List<GYTInfoDto> AddRange(List<GYTInfoDto> list)
        {
            var entities = list.Map<GYTInfo>();
            return _gytInfoDataService.AddRange(entities).Map<GYTInfoDto>().ToList();
        }

        public GYTInfoDto Edit(GYTInfoDto model)
        {
            if (model.Id.IsNullOrEmpty())
            {
                throw new CustomException("找不到指定的港运通办理业务记录，港运通ID为空");
            }

            return _gytInfoDataService.Update(model.Id, x =>
             {
                 x.BidCompanyId = model.BidCompanyId;
                 x.BidCompanyName = model.BidCompanyName;
                 x.BidDisplayName = model.BidDisplayName;
                 x.BidName = model.BidName;
                 x.BidTruckNo = model.BidTruckNo;
                 x.BusinessType = model.BusinessType;
                 x.OriginalCompanyId = model.OriginalCompanyId;
                 x.OriginalCompanyName = model.OriginalCompanyName;
                 x.BidDate = model.BidDate;
                 x.Status = model.Status;
             }).Map<GYTInfoDto>();
        }

        public bool Exists(string bidTruckNo)
        {
            return _gytInfoDataService.GetAll().Any(x => x.BidTruckNo == bidTruckNo.Trim() && x.Status == BusinessState.已办理);
        }

        public List<GYTInfoDto> GetListByBidTruckNoList(List<string> bidTruckNoList)
        {
            return _gytInfoDataService.GetAll()
                .Where(x => bidTruckNoList.Contains(x.BidTruckNo))
                .Map<GYTInfoDto>()
                .ToList();
        }

        public List<GYTInfoDto> GetList(GYTInfoSearchDto searchDto)
        {
            var query = _gytInfoDataService.GetAll();

            if (searchDto == null) return query.Map<GYTInfoDto>().ToList();

            query = Filter(searchDto, query);

            query = query.ToPageQuery(searchDto);

            return query.Map<GYTInfoDto>().ToList();
        }

        public GYTInfoDto GetByBidTruckNo(string bidTruckNo)
        {
            var entity = _gytInfoDataService
                .FirstOrDefault(x => x.BidTruckNo.Trim().ToUpper() == bidTruckNo.Trim().ToUpper());//string.Equals(x.BidTruckNo, bidTruckNo.Trim(), StringComparison.CurrentCultureIgnoreCase));
            return entity == null ? null : entity.Map<GYTInfoDto>();
        }

        private IQueryable<GYTInfo> Filter(GYTInfoSearchDto searchDto, IQueryable<GYTInfo> query)
        {
            if (!searchDto.CompanyName.IsNullOrEmpty())
            {
                query = query.Where(x => x.BidCompanyName.Contains(searchDto.CompanyName) || x.OriginalCompanyName.Contains(searchDto.CompanyName));
            }

            if (!searchDto.TruckNo.IsNullOrEmpty())
            {
                query = query.Where(x => x.BidTruckNo.Contains(searchDto.TruckNo) ||
                                         x.OriginalTruckNo.Contains(searchDto.TruckNo));
            }

            if (searchDto.Status.HasValue)
            {
                query = query.Where(x => x.Status == searchDto.Status);
            }

            if (searchDto.StartDate.HasValue)
            {
                query = query.Where(x => x.BidDate >= searchDto.StartDate);
            }

            if (searchDto.EndDate.HasValue)
            {
                query = query.Where(x => x.BidDate <= searchDto.EndDate);
            }

            return query;
        }

        public bool TargetIsUse(string truckNo)
        {
            return
                _gytInfoDataService.GetAll()
                    .Any(x => x.OriginalTruckNo == truckNo &&
                        x.BusinessType == BusinessType.以旧换新车辆 &&
                        x.Status == BusinessState.已办理);
        }

        public void SetStatus(string truckNo, BusinessState state)
        {
            var gytInfo = _gytInfoDataService.FirstOrDefault(x=>x.BidTruckNo == truckNo && x.Status == BusinessState.已办理);
            if (gytInfo == null)
            {
                return;
            }

            _gytInfoDataService.Update(gytInfo.Id, x => x.Status = state);
        }

        public GYTInfoDto Verify(string infoId)
        {
            var gytInfoDto = _gytInfoDataService.GetById(infoId);

            if (gytInfoDto.Status == BusinessState.已注销)
            {
                gytInfoDto.Status = BusinessState.已办理;
            }

            return _gytInfoDataService.Update(gytInfoDto.Map<GYTInfo>()).Map<GYTInfoDto>();
        }
    }
}
