using System.Collections.Generic;
using System.Linq;
using Zer.Entities;
using Zer.GytDataService;
using Zer.GytDto;
using Zer.GytDto.Extensions;

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
    }
}