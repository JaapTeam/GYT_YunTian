using System.Collections.Generic;
using Zer.GytDto;
using Zer.GytDto.SearchFilters;

namespace Zer.AppServices
{
    public interface IGYTInfoService : IAppService<GYTInfoDto, int>
    {
        bool Exists(string bidTruckNo);

        List<GYTInfoDto> GetList(GYTInfoSearchDto searchDto);
    }
}