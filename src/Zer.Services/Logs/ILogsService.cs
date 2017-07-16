using System.Collections.Generic;
using Zer.Services.Logs.Dto;

namespace Zer.Services.Logs
{
    public interface ILogsService:IDomainService<LogsDto,int>
    {
        List<LogsDto> GetListByFilterMatch(FilterMatchInputDto filterMatch);

        List<LogsDto> GetListByIds(int[] ids);
    }
}
