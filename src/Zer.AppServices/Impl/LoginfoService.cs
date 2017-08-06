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

        public LogInfoDto GetById(int id)
        {
            return Mapper.Map<LogInfoDto>(_logInfoDataService.GetById(id));
        }

        public List<LogInfoDto> GetAll()
        {
            return Mapper.Map<List<LogInfoDto>>(_logInfoDataService.GetAll());
        }

        public bool Add(LogInfoDto model)
        {
            throw new System.NotImplementedException();
        }

        public bool AddRange(List<LogInfoDto> list)
        {
            throw new System.NotImplementedException();
        }

        public List<LogInfoDto> GetListByFilterMatch(FilterMatchInputDto filterMatch)
        {
            throw new System.NotImplementedException();
        }

        public List<LogInfoDto> GetListByIds(int[] ids)
        {
            return Mapper.Map<List<LogInfoDto>>(_logInfoDataService.GetAll().Where(x => ids.Contains(x.Id)).ToList());
        }

        public List<LogInfoDto> GetListByUserId(int userId)
        {
            return GetAll().Where(x => x.UserId == userId).ToList();
        }
    }
}