using System.Collections.Generic;
using System.Linq;
using Zer.Entities;
using Zer.Framework.Extensions;
using Zer.GytDataService;
using Zer.GytDto;
using Zer.GytDto.Extensions;
using Zer.GytDto.SearchFilters;

namespace Zer.AppServices.Impl
{
    public class PeccancyRecrodService : IPeccancyRecrodService
    {
        private readonly IPeccancyRecrodDataService _peccancyRecrodDataService;

        public PeccancyRecrodService(IPeccancyRecrodDataService peccancyRecrodDataService)
        {
            _peccancyRecrodDataService = peccancyRecrodDataService;
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
            var insertList= _peccancyRecrodDataService.AddRange(noexistsList.Map<PeccancyInfo>()).Map<PeccancyRecrodDto>().ToList();

            insertList.AddRange(existsList);
            return insertList;
        }

        public List<PeccancyRecrodDto> GetListByFilterMatch(FilterMatchInputDto filterMatch)
        {
            throw new System.NotImplementedException();
        }

        public bool ChangeStatusById(int id)
        {
            var overLoadInfoDto = _peccancyRecrodDataService.GetById(id);
            overLoadInfoDto.Status = Status.已整改;
            return _peccancyRecrodDataService.Update(overLoadInfoDto.Map<PeccancyInfo>()).Map<PeccancyRecrodDto>().Status == Status.已整改;
        }

        public List<PeccancyRecrodDto> GetListByIds(int[] ids)
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(PeccancyRecrodDto overloadRecrodDto)
        {
            return _peccancyRecrodDataService.GetAll()
                .Any(x => x.PeccancyId == overloadRecrodDto.PeccancyId);
        }

        public List<PeccancyRecrodDto> GetList(PeccancySearchDto searchDto)
        {
            var query = _peccancyRecrodDataService.GetAll();

            if (!searchDto.TruckNo.IsNullOrEmpty())
            {
                query = query.Where(x => x.FrontTruckNo == searchDto.TruckNo);
            }

            if (searchDto.CompanyId != 0)
            {
                query = query.Where(x => x.CompanyId == searchDto.CompanyId);
            }

            if (searchDto.Status != 0)
            {
                query = query.Where(x => x.Status == searchDto.Status);
            }

            return query.Map<PeccancyRecrodDto>().ToList();
        }

        public bool ExistsCompanyName(string companyName)
        {
            return _peccancyRecrodDataService.GetAll()
                .Any(x => x.CompanyName == companyName && x.Status == Status.未整改);
        }
    }
}