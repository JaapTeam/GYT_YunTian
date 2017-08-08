using System.Collections.Generic;
using System.Linq;
using Zer.Entities;
using Zer.GytDataService;
using Zer.GytDto;
using Zer.GytDto.Extensions;

namespace Zer.AppServices.Impl
{
    public class OverloadRecrodService : IOverloadRecrodService
    {
        private readonly IOverloadRecrodDataService _overloadRecrodDataService;

        public OverloadRecrodService(IOverloadRecrodDataService overloadRecrodDataService)
        {
            _overloadRecrodDataService = overloadRecrodDataService;
        }

        public OverloadRecrodDto GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<OverloadRecrodDto> GetAll()
        {
            return _overloadRecrodDataService.GetAll().Map<OverloadRecrodDto>().ToList();
        }

        public OverloadRecrodDto Add(OverloadRecrodDto model)
        {
            throw new System.NotImplementedException();
        }

        public List<OverloadRecrodDto> AddRange(List<OverloadRecrodDto> list)
        {
            return _overloadRecrodDataService.AddRange(list.Map<OverloadInfo>()).Map<OverloadRecrodDto>().ToList();
        }

        public List<OverloadRecrodDto> GetListByFilterMatch(FilterMatchInputDto filterMatch)
        {
            throw new System.NotImplementedException();
        }

        public bool ChangedById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<OverloadRecrodDto> GetListByIds(int[] ids)
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(OverloadRecrodDto overloadRecrodDto)
        {
            return _overloadRecrodDataService.GetAll()
                .Any(x => x.PeccancyId == overloadRecrodDto.PeccancyId);
        }
    }
}