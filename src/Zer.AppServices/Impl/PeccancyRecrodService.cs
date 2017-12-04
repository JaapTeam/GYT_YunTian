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
                x.FrontTruckNo = model.FrontTruckNo;
                x.CompanyName = model.CompanyName;
                x.BehindTruckNo = model.BehindTruckNo;
                x.DriverName = model.DriverName;
                x.PeccancyDate = model.PeccancyDate;
                x.PeccancyMatter = model.PeccancyMatter;
                x.Source = model.Source;
                x.Status = model.Status;
            }).Map<PeccancyRecrodDto>();
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

            if (searchDto == null) return query.AsEnumerable().Map<PeccancyRecrodDto>().ToList();

            query = Filter(searchDto, query);

            query = query.ToPageQuery(searchDto);

            return query.AsEnumerable().Map<PeccancyRecrodDto>().ToList();
        }

        public List<PeccancyGroupByCompanyDto> GetPeccancyGroupByCompany(PeccancyWithCompanySearchDto filter)
        {
            var sourceQuery = _peccancyRecrodDataService.GetAll();//.Where(x => x.Status == Status.未整改);

            if (filter.EndDateTime.HasValue)
            {
                sourceQuery = sourceQuery.Where(x => x.PeccancyDate <= filter.EndDateTime);
            }

            if (filter.StartDateTime.HasValue)
            {
                sourceQuery = sourceQuery.Where(x => x.PeccancyDate >= filter.StartDateTime);
            }

            var query = sourceQuery.GroupBy(x => x.CompanyId, x => x)
                                   .OrderByDescending(x => x.Count(y=>y.Status == Status.未整改))
                                   .ThenByDescending(x=>x.Count())
                                   .ThenBy(x=>x.Count(y => y.Status == Status.已整改))
                                   .Where(x => x.Count(y => y.Status == Status.未整改) >= filter.MinCount);

            filter.Total = query.Count();

            var currentPagePeccancyWithCompanyIdList = query.Skip((filter.PageIndex - 1) * filter.PageSize)
                                                            .Take(filter.PageSize)
                                                            .Select(x => new
                                                            {
                                                                CompanyId = x.Key,
                                                                PeccancyRecordCount = x.Count(),
                                                                CanceledCount = x.Count(y=>y.Status == Status.已整改),
                                                                UnCanceledCount = x.Count(y=>y.Status == Status.未整改)
                                                            }).ToList();

            var companyIdList = currentPagePeccancyWithCompanyIdList.Select(x => x.CompanyId).ToList();

            var companyList = _companyInfoDataService.GetAll().Where(x => companyIdList.Contains(x.Id)).ToList();

            var resultList = new List<PeccancyGroupByCompanyDto>();

            foreach (var companyInfo in companyList)
            {
                var peccWithCom = currentPagePeccancyWithCompanyIdList.FirstOrDefault(x => x.CompanyId == companyInfo.Id);
                resultList.Add(new PeccancyGroupByCompanyDto()
                {
                    Id = companyInfo.Id,
                    CompanyName = companyInfo.CompanyName,
                    CanceledCount = peccWithCom?.CanceledCount ?? 0,
                    PeccancyRecordCount = peccWithCom?.PeccancyRecordCount ?? 0,
                    UnCanceledCount = peccWithCom?.UnCanceledCount ?? 0,
                    TraderRange = companyInfo.TraderRange
                });
            }

            return resultList.OrderByDescending(x => x.UnCanceledCount)
                             .ThenByDescending(x=>x.PeccancyRecordCount)
                             .ThenBy(x=>x.CanceledCount)
                             .ToList();
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