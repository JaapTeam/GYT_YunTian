using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Zer.GytDataService;
using Zer.GytDto;
using Zer.GytDto.Extensions;

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

        public bool Add(LngAllowanceInfoDto model)
        {
            throw new System.NotImplementedException();
        }

        public bool AddRange(List<LngAllowanceInfoDto> list)
        {
            throw new System.NotImplementedException();
        }

        public List<LngAllowanceInfoDto> GetList(FilterMatchInputDto filterMatchInput)
        {
            throw new System.NotImplementedException();
        }

        public List<LngAllowanceInfoDto> GetList(int[] idList)
        {
            throw new System.NotImplementedException();
        }
    }
}