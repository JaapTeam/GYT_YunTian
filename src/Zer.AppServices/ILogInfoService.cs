using System.Collections.Generic;
using Zer.GytDto;
using Zer.GytDto.SearchFilters;
using Zer.GytDto.Users;

namespace Zer.AppServices
{
    public interface ILogInfoService: AppServices.IAppService<LogInfoDto,int>
    {
        List<LogInfoDto> GetListByFilterMatch(AppServices.FilterMatchInputDto filterMatch);

        List<LogInfoDto> GetListByIds(int[] ids, UserInfoDto userinfo);

        List<LogInfoDto> GetListByUserId(int userId,UserInfoDto userinfo);

        List<LogInfoDto> GetAll(UserInfoDto userInfo);

        List<LogInfoDto> GetList(LogInfoSearchDto filter, UserInfoDto userInfo);
    }
}
