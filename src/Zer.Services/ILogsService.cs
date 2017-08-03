using System.Collections.Generic;
using Zer.GytDto;

namespace Zer.Services
{
    public interface ILogsService:IDomainService<LogsDto,int>
    {
        List<LogsDto> GetListByFilterMatch(FilterMatchInputDto filterMatch);

        List<LogsDto> GetListByIds(int[] ids);
    }
}
