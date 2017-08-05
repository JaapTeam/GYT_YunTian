using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Zer.GytDataService;
using Zer.GytDto;

namespace Zer.AppServices.Impl
{
    public class LogInfoService : ILogInfoService
    {
        private readonly ILogInfoDataService _logInfoDataService;

        public LogInfoService(ILogInfoDataService logInfoDataService)
        {
            _logInfoDataService = logInfoDataService;
        }

        public LogsDto GetById(int id)
        {
            return Mapper.Map<LogsDto>(_logInfoDataService.GetById(id));
        }

        public List<LogsDto> GetAll()
        {
            return Mapper.Map<List<LogsDto>>(_logInfoDataService.GetAll());
        }

        public bool Add(LogsDto model)
        {
            throw new System.NotImplementedException();
        }

        public bool AddRange(List<LogsDto> list)
        {
            throw new System.NotImplementedException();
        }

        public List<LogsDto> GetListByFilterMatch(FilterMatchInputDto filterMatch)
        {
            throw new System.NotImplementedException();
        }

        public List<LogsDto> GetListByIds(int[] ids)
        {
            return Mapper.Map<List<LogsDto>>(_logInfoDataService.GetAll().Where(x => ids.Contains(x.Id)).ToList());
        }
    }
}