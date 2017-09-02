using System.Collections.Generic;
using System.Linq;
using Zer.Entities;
using Zer.Framework.Dto;
using Zer.Framework.Extensions;
using Zer.GytDataService;
using Zer.GytDto;
using Zer.GytDto.Extensions;
using Zer.GytDto.OutputDto;
using Zer.GytDto.SearchFilters;

namespace Zer.AppServices.Impl
{
    public class PeccancyRecrodService : IPeccancyRecrodService
    {
        private readonly IPeccancyRecrodDataService _peccancyRecrodDataService;
        private readonly ICompanyInfoDataService _companyInfoDataService;

        public PeccancyRecrodService(
            IPeccancyRecrodDataService peccancyRecrodDataService,
            ICompanyInfoDataService companyInfoDataService)
        {
            _peccancyRecrodDataService = peccancyRecrodDataService;
            _companyInfoDataService = companyInfoDataService;
        }

        public PeccancyRecrodDto GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<PeccancyRecrodDto> GetAll()
        {
            return _peccancyRecrodDataService.GetAll().Map<PeccancyRecrodDto>().ToList();
        }

        public PeccancyRecrodDto Add(PeccancyRecrodDto model)
        {
            throw new System.NotImplementedException();
        }

        public List<PeccancyRecrodDto> AddRange(List<PeccancyRecrodDto> list)
        {
            var existsList = list.Where(Exists).ToList();
            var noexistsList = list.Where(x => !Exists(x)).ToList();
            var insertList = _peccancyRecrodDataService.AddRange(noexistsList.Map<PeccancyInfo>()).Map<PeccancyRecrodDto>().ToList();

            insertList.AddRange(existsList);
            return insertList;
        }

        public PeccancyRecrodDto Edit(PeccancyRecrodDto model)
        {
            return _peccancyRecrodDataService.Update(model.Id, x =>
            {
                x.BehindTruckNo = model.BehindTruckNo;
                //x.TraderRange = model.TraderRange,
                x.DriverName = model.DriverName;
                x.PeccancyDate = model.PeccancyDate;
                x.PeccancyMatter = model.PeccancyMatter;
                x.Source = model.Source;
                x.Status = model.Status;
            }).Map<PeccancyRecrodDto>();
            //.Update(model.Map<PeccancyInfo>()).Map<PeccancyRecrodDto>();
        }

        public bool ChangeStatusById(string id)
        {
            var overLoadInfoDto = _peccancyRecrodDataService.Single(x => x.Id == id);
            overLoadInfoDto.Status = Status.已整改;
            return _peccancyRecrodDataService.Update(overLoadInfoDto.Map<PeccancyInfo>()).Map<PeccancyRecrodDto>().Status == Status.已整改;
        }

        public bool Exists(PeccancyRecrodDto overloadRecrodDto)
        {
            return _peccancyRecrodDataService.GetAll()
                .Any(x => x.Id == overloadRecrodDto.Id);
        }

        public List<PeccancyRecrodDto> GetList(PeccancySearchDto searchDto)
        {
            var query = _peccancyRecrodDataService.GetAll();

            if (searchDto == null) return query.Map<PeccancyRecrodDto>().ToList();

            query = Filter(searchDto, query);

            query = query.ToPageQuery(searchDto);

            return query.Map<PeccancyRecrodDto>().ToList();
        }

        public List<PeccancyGroupByCompanyDto> GetPeccancyGroupByCompany(PeccancyWithCompanySearchDto filter)
        {
            var sourceQuery = _peccancyRecrodDataService.GetAll().Where(x => x.Status == Status.未整改);

            if (filter.EndDateTime.HasValue)
            {
                sourceQuery = sourceQuery.Where(x => x.PeccancyDate <= filter.EndDateTime);
            }

            if (filter.StartDateTime.HasValue)
            {
                sourceQuery = sourceQuery.Where(x => x.PeccancyDate >= filter.StartDateTime);
            }

            var query = sourceQuery.GroupBy(x => x.CompanyId, x => x)
                                   .OrderByDescending(x => x.Count())
                                   .Where(x => x.Count() >= filter.MinCount);

            filter.Total = query.Count();

            var peccancyCountWithCompanyId = query.Skip((filter.PageIndex - 1) * filter.PageSize)
                                                  .Take(filter.PageSize)
                                                  .Select(x => new { CompanyId = x.Key, PeccancyRecordCount = x.Count() })
                                                  .ToList();

            var companyIdList = peccancyCountWithCompanyId.Select(x => x.CompanyId).ToList();

            var result = _companyInfoDataService.GetAll().Where(x => companyIdList.Contains(x.Id)).Map<PeccancyGroupByCompanyDto>()
                .ToList();

            return result.Join(peccancyCountWithCompanyId, x => x.Id, x => x.CompanyId,
                        (companyInfo, group) => new PeccancyGroupByCompanyDto()
                        {
                            CompanyName = companyInfo.CompanyName,
                            Id = companyInfo.Id,
                            PeccancyRecordCount = group.PeccancyRecordCount,
                            TraderRange = companyInfo.TraderRange
                        }).OrderByDescending(x => x.PeccancyRecordCount).ToList();
        }

        private IQueryable<PeccancyInfo> Filter(PeccancySearchDto searchDto, IQueryable<PeccancyInfo> query)
        {
            if (!searchDto.CompanyName.IsNullOrEmpty())
            {
                query = query.Where(x => x.CompanyName.Contains(searchDto.CompanyName));
            }

            if (!searchDto.TruckNo.IsNullOrEmpty())
            {
                query = query.Where(x => x.BehindTruckNo.Contains(searchDto.TruckNo) || x.FrontTruckNo.Contains(searchDto.TruckNo));
            }

            if (searchDto.Status.HasValue)
            {
                query = query.Where(x => x.Status == searchDto.Status);
            }

            if (searchDto.StartDate.HasValue)
            {
                query = query.Where(x => x.PeccancyDate >= searchDto.StartDate);
            }

            if (searchDto.EndDate.HasValue)
            {
                query = query.Where(x => x.PeccancyDate <= searchDto.EndDate);
            }

            return query;
        }

        public bool ExistsCompanyName(string companyName)
        {
            return _peccancyRecrodDataService.GetAll()
                .Any(x => x.CompanyName == companyName && x.Status == Status.未整改);
        }

        public PeccancyRecrodDto GetByPeccancyId(string peccancyId)
        {
            return
                _peccancyRecrodDataService.GetAll()
                    .Where(x => x.Id.Equals(peccancyId))
                    .ToList()
                    .FirstOrDefault()
                    .Map<PeccancyRecrodDto>();
        }
    }
}