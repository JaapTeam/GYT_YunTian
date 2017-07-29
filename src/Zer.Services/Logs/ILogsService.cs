using System.Collections.Generic;
using Zer.Framework;
using Zer.Services.Logs.Dto;

namespace Zer.Services.Logs
{
    public interface ILogsService : IAppService
    {
        List<LogsDto> GetListByFilterMatch(FilterMatchInputDto filterMatch);

        List<LogsDto> GetListByIds(int[] ids);


        List<LogsDto> GetList(int[] idList);

        LogsDto GetById(int id);

        List<LogsDto> GetAll();

        bool Add(LogsDto model);

        bool AddRange(List<LogsDto> list);
    }
}
