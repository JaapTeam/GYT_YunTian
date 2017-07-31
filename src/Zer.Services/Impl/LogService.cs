using System.Collections.Generic;
using AutoMapper;
using Zer.GytDataService;
using Zer.GytDto;

namespace Zer.Services.Impl
{
    public class LogService:ILogsService
    {
        private readonly ILogDataService _logDataService;

        public LogService(ILogDataService logDataService)
        {
            _logDataService = logDataService;
        }

        public List<LogsDto> GetListByFilterMatch(FilterMatchInputDto filterMatch)
        {
            throw new System.NotImplementedException();
        }

        public List<LogsDto> GetListByIds(int[] ids)
        {
            throw new System.NotImplementedException();
        }

        public List<LogsDto> GetList(int[] idList)
        {
            throw new System.NotImplementedException();
        }

        public LogsDto GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<LogsDto> GetAll()
        {
            return Mapper.Map<List<LogsDto>>(_logDataService.GetAll());
        }

        public bool Add(LogsDto model)
        {
            throw new System.NotImplementedException();
        }

        public bool AddRange(List<LogsDto> list)
        {
            throw new System.NotImplementedException();
        }
    }
}