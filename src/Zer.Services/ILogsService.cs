using System.Collections.Generic;
using Zer.GytDto;

namespace Zer.AppServices
{
    public interface ILogsService: AppServices.IDomainService<LogsDto,int>
    {
        List<LogsDto> GetListByFilterMatch(AppServices.FilterMatchInputDto filterMatch);

        List<LogsDto> GetListByIds(int[] ids);
    }
}
