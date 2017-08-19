using System.Collections.Generic;
using Zer.GytDto;
using Zer.GytDto.SearchFilters;

namespace Zer.AppServices
{
    public interface IGYTInfoService : IAppService<GYTInfoDto, int>
    {
        bool Exists(string bidTruckNo);

        List<GYTInfoDto> GetList(GYTInfoSearchDto searchDto);

        /// <summary>
        /// 检查车牌以旧换新指标是否已使用
        /// </summary>
        /// <param name="truckNo"></param>
        /// <returns></returns>
        bool TargetIsUse(string truckNo);

        GYTInfoDto Verify(int infoId);
    }
}