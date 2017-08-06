using System.Collections.Generic;
using Zer.GytDto;

namespace Zer.AppServices
{
    public interface ILogInfoService: AppServices.IAppService<LogInfoDto,int>
    {
        List<LogInfoDto> GetListByFilterMatch(AppServices.FilterMatchInputDto filterMatch);

        List<LogInfoDto> GetListByIds(int[] ids);

        List<LogInfoDto> GetListByUserId(int userId);
    }
}
